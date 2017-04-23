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
    /// Interaction logic for ViewRelativesPage.xaml
    /// </summary>
    public partial class ViewRelativesPage : Page
    {
        FreshStartController freshStartController;
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewRelativesPage()
        {
            freshStartController = new FreshStartController();//new up our controller
            InitializeComponent();
            setupFormElements();//setup the data grid
        }
        /// <summary>
        /// This function sends you back to the main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
        /// <summary>
        /// This function redirects the user to the edit relaive fresh start page...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditFreshStart_Click(object sender, RoutedEventArgs e)
        {
            var relative = RealtivesDataGrid.Items.GetItemAt(RealtivesDataGrid.SelectedIndex);//fetch the relative fresh start row that was clicked
            var relativeID = Convert.ToInt32(relative.GetType().GetProperty("RelativeID").GetValue(relative).ToString());//fetch the relativeID from the generic object
            //navigate to relative edit page...
            this.NavigationService.Navigate(new AddRelativeFreshStartPage(relativeID));
        }
        /// <summary>
        /// This function populates the data grid with relative information..
        /// </summary>
        private void setupFormElements()
        {
            RealtivesDataGrid.ItemsSource = freshStartController.GetAllRelatives();
        }
    }
}
