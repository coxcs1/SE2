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
    /// Interaction logic for AddRelativeFreshStartPage.xaml
    /// </summary>
    public partial class AddRelativeFreshStartPage : Page
    {
        FreshStartController freshStartController;

        public AddRelativeFreshStartPage()
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
            setupFormElements();
        }
        /// <summary>
        /// This function populates all combo boxes for the add relatives page...
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.ItemsSource = freshStartController.GetClientsForComboBox();
            FreshStartComboBox.ItemsSource = freshStartController.GetAllFreshStarts();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
