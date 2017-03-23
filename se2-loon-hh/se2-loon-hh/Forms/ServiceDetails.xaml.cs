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
        ServiceRequested serviceRequested;

        public ServiceDetails(int serviceID)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            serviceRequested = serviceController.GetServiceRequested(serviceID);//fetch the service requested from the database
            Console.WriteLine(serviceRequested.Client.FirstName);
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
