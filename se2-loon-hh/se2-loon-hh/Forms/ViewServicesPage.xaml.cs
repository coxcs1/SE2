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
    /// Interaction logic for ViewServicesPage.xaml
    /// </summary>
    public partial class ViewServicesPage : Page
    {
        ServiceController serviceController;
        
        public ViewServicesPage()
        {
            serviceController = new ServiceController();
            InitializeComponent();
            setupComponents();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }

        private void ServiceDetails_Click(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.Items.GetItemAt(ServicesDataGrid.SelectedIndex);//fetch the service row that was clicked
            var serviceRequestedID = Convert.ToInt32(service.GetType().GetProperty("ServiceID").GetValue(service).ToString());//fetch the serviceID from the generic object
            //navigate to service detail page
            this.NavigationService.Navigate(new ServiceDetails(serviceRequestedID));
        }

        /// <summary>
        /// Setup each component for the view services page.
        /// This includes adding all the services to the data grid for display
        /// </summary>
        private void setupComponents()
        {
            foreach (var serviceItem in serviceController.GetAllServices())
            {
                ServicesDataGrid.Items.Add(serviceItem);
            }
        }
    }
}
