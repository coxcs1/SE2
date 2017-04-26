using se2_loon_hh.Controllers;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for EmergencyDetails.xaml
    /// </summary>
    public partial class EmergencyDetails : Page
    {
        EmergencyController emergencyController;
        Emergency emergency;

        public EmergencyDetails(int id)
        {
            InitializeComponent();
            PageTitle.Text = "Emergency Aid Details";
            emergencyController = new EmergencyController();
            emergency = emergencyController.viewEmergency(id);
            setupComponents();
        }

        private void setupComponents()
        {
            EmergencyDetailsItem.Items.Add(new
            {
                Label = "First Name",
                Text = emergency.FirstName
            });

            EmergencyDetailsItem.Items.Add(new
            {
                Label = "Last Name",
                Text = emergency.LastName
            });

            EmergencyDetailsItem.Items.Add(new
            {
                Label = "Date Given",
                Text = emergency.DateReceived
            });

            EmergencyDetailsItem.Items.Add(new
            {
                Label = "Items Given",
                Text = emergency.ItemsReceived
            });

            EmergencyDetailsItem.Items.Add(new
            {
                Label = "Comments",
                Text = emergency.Comment
            });
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewEmergencies());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmergency(Convert.ToInt32(emergency.Id)));
        }
    }
}
