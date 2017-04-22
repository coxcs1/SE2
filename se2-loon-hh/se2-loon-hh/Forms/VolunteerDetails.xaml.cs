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
    /// Interaction logic for VolunteerDetails.xaml
    /// </summary>
    public partial class VolunteerDetails : Page
    {
        VolunteerController VolunteerController;
        Volunteer Volunteer;

        public VolunteerDetails(int VolunteerID)
        {
            VolunteerController = new VolunteerController();//initialize controller
            InitializeComponent();//setup components
            Volunteer = VolunteerController.GetVolunteer(VolunteerID);
            setupComponents();//populate the data grid with volunteer information
        }

        private void setupComponents()
        {
            VolunteerDetailsList.Items.Add(new
            {
                Label = "First Name: ",
                Text = Volunteer.FirstName
            });

            VolunteerDetailsList.Items.Add(new
            {
                Label = "Last Name: ",
                Text = Volunteer.LastName
            });

            VolunteerDetailsList.Items.Add(new
            {
                Label = "Date Received: ",
                Text = Volunteer.DateReceived
            });

            VolunteerDetailsList.Items.Add(new
            {
                Label = "Hours: ",
                Text = Volunteer.Hours
            });

            VolunteerDetailsList.Items.Add(new
            {
                Label = "Service: ",
                Text = Volunteer.Service
            });

            VolunteerDetailsList.Items.Add(new
            {
                Label = "Comment: ",
                Text = Volunteer.Comment
            });
        }
        /// <summary>
        /// When the back button is clicked, navigate back to the view Volunteers page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewVolunteerPage());
        }
    }
}