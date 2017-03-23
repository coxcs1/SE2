using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using se2_loon_hh.Controllers;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        ServiceController serviceController;
        
        public AddServicePage()
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            setupFormElements();//populate the form
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //create and populate the service using manually ugly property calls
            var service = new Service();
            service.Name = ServiceName.Text;
            service.Type = ServiceType.Text;
            service.Description = ServiceDescription.Text;
            service.DateArrived = DateArrivedDatePicker.Text;
            service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            service.NewWalkIn = Convert.ToInt64(NewWalkIn.IsChecked);
            service.WalkIn = Convert.ToInt64(WalkIn.IsChecked);
            service.TelephoneAfterHrs = Convert.ToInt64(TelephoneAfterHours.IsChecked);
            service.PrankCall = Convert.ToInt64(PrankCall.IsChecked);
            service.OffSite = Convert.ToInt64(OffSite.IsChecked);
            service.RepresentedBySomeoneElse = Convert.ToInt64(RepresentedBySomeoneElse.IsChecked);
            service.OutgoingCallMailEmail = Convert.ToInt64(OutgoingCallMailEmail.IsChecked);
            service.Email = Convert.ToInt64(Email.IsChecked);
            service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            //create as many donations as needed and link them to a service
            foreach (var item in DonationsDataGrid.Items)
            {
                //create the donation
                var donation = new Donation();
                donation.Name = item.GetType().GetProperty("Name").GetValue(item).ToString();
                donation.Type = item.GetType().GetProperty("Type").GetValue(item).ToString();
                donation.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                //add the donation to the service
                service.Donations.Add(donation);
            }
            //create the requested service and pass it to the controller
            var serviceRequested = new ServiceRequested();
            serviceRequested.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);//attach the client to the requested service
            serviceRequested.Service = service;
            serviceRequested.DateReceived = DateArrivedDatePicker.Text;
            serviceController.CreateService(service, serviceRequested);//persist the service into the database
            this.NavigationService.Navigate(new MainPage());//go to home page
        }

        /// <summary>
        /// This function uses the ComboBoxPairs class to store
        /// each client in the combo box as a key/value.
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = serviceController.GetClientsForComboBox();//store ComboBoxPairs data structure
            DonationType.DisplayMemberPath = "_Value";//what to display
            DonationType.SelectedValuePath = "_Key";//what to use when passing data to back end
            DonationType.ItemsSource = serviceController.GetDonationTypes();//load up donation types for the donation type combo box
        }

        private void AddDonationButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Name = DonationName.Text, Type = DonationType.SelectedItem.ToString(), Comment = DonationComment.Text };
            DonationsDataGrid.Items.Add(row);
        }

        private void RemoveDonation_Click(object sender, RoutedEventArgs e)
        {
            DonationsDataGrid.Items.RemoveAt(DonationsDataGrid.SelectedIndex);
        }
    }
}
