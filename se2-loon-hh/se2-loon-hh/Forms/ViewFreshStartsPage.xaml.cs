using se2_loon_hh.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <summary>
        /// This function redirects to the add fresh start page with an id for editing an existing fresh start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditFreshStart_Click(object sender, RoutedEventArgs e)
        {
            var freshStart = FreshStartsDataGrid.Items.GetItemAt(FreshStartsDataGrid.SelectedIndex);//fetch the fresh start row that was clicked
            var freshStartID = Convert.ToInt32(freshStart.GetType().GetProperty("FreshStartID").GetValue(freshStart).ToString());//fetch the freshStartID from the generic object
            //navigate to service detail page
            this.NavigationService.Navigate(new AddFreshStart(freshStartID));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void setupDataGrid()
        {
            FreshStartsDataGrid.ItemsSource = freshStartController.GetFreshStartsByType();
            FreshStartsDataGrid.Items.SortDescriptions.Add(new SortDescription(FreshStartsDataGrid.Columns[0].SortMemberPath, ListSortDirection.Ascending));
        }
    }
}
