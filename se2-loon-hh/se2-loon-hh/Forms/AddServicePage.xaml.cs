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

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        ServiceController serviceController;
        
        public AddServicePage()
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            setupFormElements();//populate the form
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Process Form via controller...");
        }

        /// <summary>
        /// This function uses the ComboBoxPairs class to store
        /// each client in the combo box as a key/value.
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = serviceController.GetClientsForComboBox();//store ComboBoxPairs data structure
        }

        private void AddDonationButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Name = DonationName.Text, Type = DonationType.Text, Comment = DonationComment.Text };
            DonationsDataGrid.Items.Add(row);
        }

        private void RemoveDonation_Click(object sender, RoutedEventArgs e)
        {
            DonationsDataGrid.Items.RemoveAt(DonationsDataGrid.SelectedIndex);
        }
    }
}
