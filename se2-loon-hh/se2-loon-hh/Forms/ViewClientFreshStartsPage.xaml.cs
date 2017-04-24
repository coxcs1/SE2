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
    /// Interaction logic for ViewClientFreshStarts.xaml
    /// </summary>
    public partial class ViewClientFreshStartsPage : Page
    {
        FreshStartController freshStartController;
        
        public ViewClientFreshStartsPage()
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
            populateForm();
        }
        /// <summary>
        /// This function navigates a user to the edit page for a client fresh start..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClientFreshStart_Click(object sender, RoutedEventArgs e)
        {
            var clientFreshStart = ClientFreshStartsDataGrid.Items.GetItemAt(ClientFreshStartsDataGrid.SelectedIndex);//fetch the relative fresh start row that was clicked
            var clientFreshStartID = Convert.ToInt32(clientFreshStart.GetType().GetProperty("ClientFreshStartID").GetValue(clientFreshStart).ToString());//fetch the relativeID from the generic object
            //navigate to client fresh start edit page...
            this.NavigationService.Navigate(new AddClientFreshStartPage(clientFreshStartID));
        }
        /// <summary>
        /// This function navigates a user to a details page for a client fresh start...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientFreshStartDetails_Click(object sender, RoutedEventArgs e)
        {
            var clientFreshStart = ClientFreshStartsDataGrid.Items.GetItemAt(ClientFreshStartsDataGrid.SelectedIndex);//fetch the relative fresh start row that was clicked
            var clientFreshStartID = Convert.ToInt32(clientFreshStart.GetType().GetProperty("ClientFreshStartID").GetValue(clientFreshStart).ToString());//fetch the relativeID from the generic object
            //navigate to client fresh start edit page...
            this.NavigationService.Navigate(new ClientFreshStartDetailsPage(clientFreshStartID));
        }
        /// <summary>
        /// This function loads up the client fresh starts into a data grid..
        /// </summary>
        private void populateForm()
        {
            ClientFreshStartsDataGrid.ItemsSource = freshStartController.GetAllClientFreshStarts();
        }

        /// <summary>
        /// This function returns the user to the home page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
