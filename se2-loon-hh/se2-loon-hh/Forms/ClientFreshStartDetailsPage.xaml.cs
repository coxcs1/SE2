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
    /// Interaction logic for ClientFreshStartDetails.xaml
    /// </summary>
    public partial class ClientFreshStartDetailsPage : Page
    {
        FreshStartController freshStartController;
        ClientFreshStart client;

        public ClientFreshStartDetailsPage(int clientFreshStartID)
        {
            freshStartController = new FreshStartController();//new up our fresh start controller
            InitializeComponent();
            client = freshStartController.GetClientFreshStart(clientFreshStartID);//find the client fresh start in the database
            populateForm();//load the data into the form
        }
        /// <summary>
        /// This function loads up the details list and the support groups data grid
        /// </summary>
        private void populateForm()
        {
            ClientFreshStartDetailsList.Items.Add(new {
                Label = "Client: ",
                Text = client.Client.FirstName + " " + client.Client.MiddleName + " " + client.Client.LastName
            });
            ClientFreshStartDetailsList.Items.Add(new
            {
                Label = "Fresh Start: ",
                Text = client.FreshStart.Name
            });
            ClientFreshStartDetailsList.Items.Add(new
            {
                Label = "Date: ",
                Text = client.Date
            });
            ClientFreshStartDetailsList.Items.Add(new
            {
                Label = "Status: ",
                Text = client.Status
            });
            //load up each support group
            foreach (var supportGroup in client.SupportGroups)
            {
                SupportGroupsDataGrid.Items.Add(new {
                    Month = supportGroup.MonthAttended,
                    Year = Convert.ToString(supportGroup.Year)
                });
            }
        }
        /// <summary>
        /// This function takes the user back to the view client fresh starts page..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewClientFreshStartsPage());
        }
    }
}
