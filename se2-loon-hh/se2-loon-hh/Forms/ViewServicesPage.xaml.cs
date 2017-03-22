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
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }

        private void ServiceDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void setupComponents()
        {

        }
    }
}
