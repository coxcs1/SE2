using se2_loon_hh.Controllers;
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

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for ServiceDetails.xaml
    /// </summary>
    public partial class ServiceDetails : Page
    {
        ServiceController serviceController;
        ServiceInfo serviceRequested;

        public ServiceDetails(int serviceID)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            serviceRequested = serviceController.GetServiceInfo(serviceID);//fetch the service requested from the database
            setupComponents();//populate the data grid with client information
        }

        private void setupComponents()
        {
            //ServiceDetailsList.Items.Add(new {
            //    Label = "Client: ",
            //    Text = serviceRequested.Client.FirstName + " " + serviceRequested.Client.MiddleName + " " + serviceRequested.Client.LastName
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Date Arrived: ",
            //    Text = serviceRequested.Service.DateArrived
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Name: ",
            //    Text = serviceRequested.Service.Name
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Type: ",
            //    Text = serviceRequested.Service.Type
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Description: ",
            //    Text = serviceRequested.Service.Description
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "New Walk In: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.NewWalkIn)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Telephone After Hours: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.TelephoneAfterHrs)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Prank Call: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.PrankCall)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Email: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.Email)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Outgoing Call Mail Email: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.OutgoingCallMailEmail)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Off Site: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.OffSite)) ? "Yes" : "No"
            //});
            //ServiceDetailsList.Items.Add(new
            //{
            //    Label = "Represented By Someone Else: ",
            //    Text = (Convert.ToBoolean(serviceRequested.Service.RepresentedBySomeoneElse)) ? "Yes" : "No"
            //});
            ////add each dontation to the data grid
            //foreach (var donation in serviceRequested.Service.Donations)
            //{
            //    DonationsDataGrid.Items.Add(new {
            //        Name = donation.Name,
            //        Type = donation.Type,
            //        Comment = donation.Comment
            //    });
            //}
        }
        /// <summary>
        /// When the back button is clicked, navigate back to the view services page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewServicesPage());
        }
    }
}
