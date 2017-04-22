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
using se2_loon_hh.Controllers;
using se2_loon_hh.Forms.FormsAPI;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation;


namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for ViewProgressPointsPage.xaml
    /// </summary>
    public partial class ViewProgressPointsPage : Page
    {
        ProgressPointsController pointsController;

        public ViewProgressPointsPage()
        {
            pointsController = new ProgressPointsController();
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
        /// This function navigates to the add service page and passes
        /// the ID of the service requested to populate that form for editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditPoints_Click(object sender, RoutedEventArgs e)
        {
            var points = PointsDataGrid.Items.GetItemAt(PointsDataGrid.SelectedIndex);//fetch the points row that was clicked
            var pointsID = Convert.ToInt32(points.GetType().GetProperty("PointsID").GetValue(points).ToString());//fetch the serviceID from the generic object
            this.NavigationService.Navigate(new EntryProgressPointsPage(pointsID));
        }



        /// <summary>
        /// Setup each component for the view points page.
        /// This includes adding all the points to the data grid for display
        /// </summary>
        private void setupComponents()
        {
            foreach (var pointsItem in pointsController.GetAllPoints())
            {
                PointsDataGrid.Items.Add(pointsItem);
            }
        }
    }
}