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
    /// Interaction logic for ClientListPage.xaml
    /// </summary>
    public partial class ClientListPage : Page
    {
        private ClientController clientCont;

        public ClientListPage()
        {
            clientCont = new ClientController();

            InitializeComponent();

            clientList.ItemsSource = clientCont.GetClientList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddClientPage());
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the button that is clicked and gets the tag, which has the 
            //client's id
            Button b = ((Button)e.OriginalSource);
            int clientId = Int32.Parse(b.Tag.ToString());
            NavigationService.Navigate(new ViewClientPage(clientId));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the button that is clicked and gets the tag, which has the 
            //client's id
            Button b = ((Button)e.OriginalSource);
            int clientId = Int32.Parse(b.Tag.ToString());
            NavigationService.Navigate(new AddClientPage(clientId));
        }
    }
}