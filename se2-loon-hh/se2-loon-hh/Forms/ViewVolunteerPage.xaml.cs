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
    /// Interaction logic for ViewVolunteerPage.xaml
    /// </summary>
    public partial class ViewVolunteerPage : Page
    {
        VolunteerController volunteerController;

        public ViewVolunteerPage()
        {
            volunteerController = new VolunteerController();
            InitializeComponent();
            setupComponents();
        }
        /// <summary>
        /// This function goes back to the home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }
        /// <summary>
        /// This function passes the Volunteer requested ID over to the 
        /// Volunteer details page to display all information related to a Volunteer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolunteerDetails_Click(object sender, RoutedEventArgs e)
        {
            var Volunteer = VolunteersDataGrid.Items.GetItemAt(VolunteersDataGrid.SelectedIndex);//fetch the Volunteer row that was clicked
            var VolunteerID = Convert.ToInt32(Volunteer.GetType().GetProperty("ID").GetValue(Volunteer).ToString());//fetch the VolunteerID from the generic object
            //navigate to Volunteer detail page
            this.NavigationService.Navigate(new VolunteerDetails(VolunteerID));
        }
        /// <summary>
        /// This function navigates to the add Volunteer page and passes
        /// the ID of the Volunteer requested to populate that form for editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditVolunteer_Click(object sender, RoutedEventArgs e)
        {
            var Volunteer = VolunteersDataGrid.Items.GetItemAt(VolunteersDataGrid.SelectedIndex);//fetch the Volunteer row that was clicked
            var VolunteerID = Convert.ToInt32(Volunteer.GetType().GetProperty("ID").GetValue(Volunteer).ToString());//fetch the VolunteerID from the generic object
            this.NavigationService.Navigate(new AddVolunteerPage(VolunteerID));
        }

        /// <summary>
        /// Setup each component for the view Volunteers page.
        /// This includes adding all the Volunteers to the data grid for display
        /// </summary>
        private void setupComponents()
        {
            foreach (var VolunteerItem in volunteerController.GetAllVolunteers())
            {
                VolunteersDataGrid.Items.Add(VolunteerItem);
            }
        }
    }
}