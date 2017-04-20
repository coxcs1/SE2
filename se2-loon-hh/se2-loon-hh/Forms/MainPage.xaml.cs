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
    public partial class MainPage : Page
    {
        public MainPage()
        { 
            InitializeComponent();
            textBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin nec volutpat enim. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris varius euismod sapien, eget bibendum sem pretium quis. Suspendisse potenti. Nam turpis ante, pretium sit amet ligula dictum, dapibus pharetra ligula. Morbi luctus erat tortor, at tempor ante bibendum in. Pellentesque vel dolor eleifend, consectetur libero eu, porttitor arcu. Maecenas lobortis nisi felis, ut viverra nunc congue non. Aliquam convallis rutrum eros non interdum. Donec ut lectus id nisi fermentum sagittis sit amet eu est. Nulla facilisi. Ut dui dolor, lacinia sit amet congue vel, volutpat id odio.";
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddClientPage());
        }

        private void ViewClientsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientListPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void HelpMenu_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }

        private void AddClientMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddClientPage());
        }

        private void ViewClientsMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientListPage());
        }

        private void ViewServices_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewServicesPage());
        }
    }
}
