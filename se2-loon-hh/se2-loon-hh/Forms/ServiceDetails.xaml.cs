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
        ServiceInfo service;

        public ServiceDetails(int serviceID)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            service = serviceController.GetServiceInfo(serviceID);//fetch the service requested from the database
            setupComponents();//populate the data grid with client information
        }
        /// <summary>
        /// This function adds all necessary items to the service details list and the items and services requested data grids.
        /// </summary>
        private void setupComponents()
        {
            ServiceDetailsList.Items.Add(new
            {
                Label = "Client: ",
                Text = service.Client.FirstName + " " + service.Client.MiddleName + " " + service.Client.LastName
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Date: ",
                Text = service.Date
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "New Walk In: ",
                Text = (Convert.ToBoolean(service.NewWalkIn)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Telephone After Hours: ",
                Text = (Convert.ToBoolean(service.PhoneAfterHrs)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Prank Call: ",
                Text = (Convert.ToBoolean(service.PrankCall)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Email: ",
                Text = (Convert.ToBoolean(service.Email)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Outgoing Call Mail Email: ",
                Text = (Convert.ToBoolean(service.OutgoingCallMailEmail)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Off Site: ",
                Text = (Convert.ToBoolean(service.OffSite)) ? "Yes" : "No"
            });
            ServiceDetailsList.Items.Add(new
            {
                Label = "Represented By Someone Else: ",
                Text = (Convert.ToBoolean(service.RepBySomeoneElse)) ? "Yes" : "No"
            });
            //add each item requested to the data grid
            foreach (var item in service.ItemRequesteds)
            {
                ItemsRequestedDataGrid.Items.Add(new
                {
                    Name = item.ItemName,
                    Comment = item.Comment
                });
            }
            //add each service requested to the data grid
            foreach (var serviceRequested in service.ServiceRequesteds)
            {
                ServicesRequestedDataGrid.Items.Add(new
                {
                    Name = serviceRequested.ServiceName,
                    Comment = serviceRequested.Comment
                });
            }
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
