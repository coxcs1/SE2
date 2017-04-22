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
    /// Interaction logic for ViewFreshStartsPage.xaml
    /// </summary>
    public partial class ViewFreshStartsPage : Page
    {
        FreshStartController freshStartController;
        public ViewFreshStartsPage()
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
            setupDataGrid();
        }

        private void EditFreshStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FreshStartDetails_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void setupDataGrid()
        {
            FreshStartsDataGrid.ItemsSource = freshStartController.GetFreshStartsByType();
        }
    }
}
