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
            textBlock.FontSize = 14;
            textBlock.Text = "From the Founder of Hope House, Wes Patten:  Hope House was born out of a dream that I had in August of 1998. Hope House didn't begin for another 3 ½ years, but God was preparing my family and I to make a huge step of faith to begin this ministry. I was a Pastor in Seattle, WA and had always spoken out against abortion, but felt like God was calling me to do more… to go beyond just standing against something (abortion, which I certainly do) and to stand for something; to stand for life!\n\n" +
                             "The dream continued to swell in my heart to the point that I could no longer ignore it and I began to search out the place where God would make Hope House a reality. In the late 1980's I attended graduate school in East Tennessee. I loved the area and learned that there was not an organization like Hope House around, but one was certainly needed. In late August 2001 we left our jobs/schools, our home, our church, our family, and our friends in Seattle for East Tennessee to begin Hope House. We had no place to live, we had no financial support, we didn't have work, and our children were already 2 weeks late to start school that fall. However, we believed fully that this was the time and the place that God wanted to start Hope House, so we trusted Him to provide all that we needed. And He did!\n\n" +
                             "Hope House officially started in January of 2002 in our small rental home. We took our first girl in and began to get support from churches and individuals who shared our vision for Hope House. For the next 18 months, we had girls come in and out of our home getting the support they needed and giving their babies the opportunity for life.\n\n" +
                             "In August of 2003, we were able to purchase our first maternity home with the help of some investors so we could expand our capacity to reach out to more young ladies in crisis pregnancy. We have not only been able to reach numerous young ladies in residence, but we are also providing needy families in our community with the resources they need for their babies.\n\n" +
                             "2017 begins our 15th year in the ministry and what an incredible journey it has been! Today, Hope House operates a maternity home, transitional apartment housing as well as a community pregnancy resource center.";
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

        private void AddServiceMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddServicePage());
        }

        private void ViewClientsMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientListPage());
        }

        private void ViewServiceMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewServicesPage());
        }

        private void ViewServices_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewServicesPage());
        }

        private void AddEmergencyMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmergency());
        }

        private void ViewEmergencyMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewEmergencies());
        }

        private void AddVolunteerMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddVolunteerPage());
        }

        private void AddClassMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddFreshStart());
        }

        private void ViewClassMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewFreshStartsPage());
        }

        private void ViewRelativesMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewRelativesPage());
        }

        private void AddRelativeMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddRelativeFreshStartPage());
        }

        private void AddClientFreshStartMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddClientFreshStartPage());
        }

        private void ViewClientsFreshStartMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewClientFreshStartsPage());
        }

        private void ViewVolunteerMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewVolunteerPage());
        }

        private void EntryPointsMenu_Click(object sender, RoutedEventArgs e)
        {
             this.NavigationService.Navigate(new EntryProgressPointsPage());
        }
      
        private void ViewPointsMenu_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewProgressPointsPage());
        }
    }
}