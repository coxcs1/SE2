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
using se2_loon_hh.Forms.FormsAPI;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        ServiceController serviceController;//Handles all persistence layer logic for services and donations
        ServiceRequested serviceRequested;//if the form is receiving an ID then populate this attribute
        FormValidator ServiceValidator { get; set; }//this serves as a validator for the entire form
        bool edit = false;//whether or not the request is to edit a service (Default false)

        
        public AddServicePage(int serviceRequestedID = 0)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            setupFormElements();//populate the form
            PageTitle.Text = "Add Service";//set the page title
            if (serviceRequestedID > 0)
            {
                PageTitle.Text = "Edit Service";//change the page title if it's an edit
                serviceRequested = serviceController.GetServiceRequested(serviceRequestedID);//fetch the service requested from the database
                populateFormElements();//populate each form element as needed
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }
        /// <summary>
        /// Checks to see if the service requested is null or not and
        /// either updates or inserts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            populateService();//fill up the service requested attribute
            if (isValid())
            {
                DataContext = ServiceValidator.ErrorMessages;
                foreach (var message in ServiceValidator.ErrorMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
                Console.WriteLine("Form is valid!");
                storeService();//save the service
                this.NavigationService.Navigate(new ViewServicesPage());//go to home page
            }
            else
            {
                DataContext = ServiceValidator.ErrorMessages;
                foreach (var message in ServiceValidator.ErrorMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
                Console.WriteLine("Form is not valid!");
            }
        }

        private bool isValid()
        {
            List<FormsAPI.Validation.Validation> validators = new List<FormsAPI.Validation.Validation>();
            LengthValidator serviceNameValidator = new LengthValidator(8, this.serviceRequested.Service.Name, ServiceName, "Service name must be 8 characters.");
            validators.Add(serviceNameValidator);
            //create validation layer
            ServiceValidator = new FormValidator(validators);

            return (ServiceValidator.isValid()) ? true : false;
        }

        /// <summary>
        /// This function populates the service requested attribute.
        /// </summary>
        private void populateService()
        {
            //if the service requested is null then create a new one
            //else the service is being edited
            if (this.serviceRequested == null)
            {
                this.serviceRequested = new ServiceRequested();
                this.serviceRequested.Service = new Service();
            }
            else
            {
                edit = true;
            }

            //modify the existing service with more hidious property calls...
            this.serviceRequested.Service.Name = ServiceName.Text;
            this.serviceRequested.Service.Type = ServiceType.Text;
            this.serviceRequested.Service.Description = ServiceDescription.Text;
            this.serviceRequested.Service.DateArrived = DateArrivedDatePicker.Text;
            this.serviceRequested.Service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            this.serviceRequested.Service.NewWalkIn = Convert.ToInt64(NewWalkIn.IsChecked);
            this.serviceRequested.Service.WalkIn = Convert.ToInt64(WalkIn.IsChecked);
            this.serviceRequested.Service.TelephoneAfterHrs = Convert.ToInt64(TelephoneAfterHours.IsChecked);
            this.serviceRequested.Service.PrankCall = Convert.ToInt64(PrankCall.IsChecked);
            this.serviceRequested.Service.OffSite = Convert.ToInt64(OffSite.IsChecked);
            this.serviceRequested.Service.RepresentedBySomeoneElse = Convert.ToInt64(RepresentedBySomeoneElse.IsChecked);
            this.serviceRequested.Service.OutgoingCallMailEmail = Convert.ToInt64(OutgoingCallMailEmail.IsChecked);
            this.serviceRequested.Service.Email = Convert.ToInt64(Email.IsChecked);
            this.serviceRequested.Service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            this.serviceRequested.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);//attach the client to the requested service
            this.serviceRequested.DateReceived = DateArrivedDatePicker.Text;
            this.serviceRequested.Service.Donations.Clear();//clear the list of donations
            //create as many donations as needed and link them to a service
            foreach (var item in DonationsDataGrid.Items)
            {
                //create the donation
                var donation = new Donation();
                donation.Name = item.GetType().GetProperty("Name").GetValue(item).ToString();
                donation.Type = item.GetType().GetProperty("Type").GetValue(item).ToString();
                donation.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                //add the donation to the service
                this.serviceRequested.Service.Donations.Add(donation);
            }
        }
        /// <summary>
        /// This function persists the service requested object into the database.
        /// </summary>
        private void storeService()
        {
            //call approriate controller action
            if (edit)
            {
                serviceController.EditService(serviceRequested);
            }
            else
            {
                serviceController.CreateService(serviceRequested.Service, serviceRequested);
            }
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
        /// <summary>
        /// This function adds a row to the dontations data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDonationButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Name = DonationName.Text, Type = DonationType.SelectedItem.ToString(), Comment = DonationComment.Text };
            DonationsDataGrid.Items.Add(row);
        }
        /// <summary>
        /// This function removes a row from the donations data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDonation_Click(object sender, RoutedEventArgs e)
        {
            DonationsDataGrid.Items.RemoveAt(DonationsDataGrid.SelectedIndex);
        }
        /// <summary>
        /// This function populates the form.
        /// </summary>
        private void populateFormElements()
        {
            ServiceName.Text = serviceRequested.Service.Name;
            ServiceType.Text = serviceRequested.Service.Type;
            ServiceDescription.Text = serviceRequested.Service.Description;
            ClientComboBox.Text = serviceRequested.Client.FirstName + " " + serviceRequested.Client.MiddleName + " " + serviceRequested.Client.LastName;
            DateArrivedDatePicker.Text = serviceRequested.Service.DateArrived;
            NewContact.IsChecked = Convert.ToBoolean(serviceRequested.Service.NewContact);
            WalkIn.IsChecked = Convert.ToBoolean(serviceRequested.Service.WalkIn);
            NewWalkIn.IsChecked = Convert.ToBoolean(serviceRequested.Service.NewWalkIn);
            TelephoneAfterHours.IsChecked = Convert.ToBoolean(serviceRequested.Service.TelephoneAfterHrs);
            PrankCall.IsChecked = Convert.ToBoolean(serviceRequested.Service.PrankCall);
            OffSite.IsChecked = Convert.ToBoolean(serviceRequested.Service.OffSite);
            RepresentedBySomeoneElse.IsChecked = Convert.ToBoolean(serviceRequested.Service.RepresentedBySomeoneElse);
            OutgoingCallMailEmail.IsChecked = Convert.ToBoolean(serviceRequested.Service.OutgoingCallMailEmail);
            Email.IsChecked = Convert.ToBoolean(serviceRequested.Service.Email);
            //populate the donation data grid
            foreach (var donation in serviceRequested.Service.Donations)
            {
                DonationsDataGrid.Items.Add(new { Name = donation.Name, Type = donation.Type, Comment = donation.Comment });
            }
        }
    }
}
