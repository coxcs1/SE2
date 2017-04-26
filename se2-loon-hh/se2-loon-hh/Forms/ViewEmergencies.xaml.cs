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
    /// Interaction logic for ViewEmergencies.xaml
    /// </summary>
    public partial class ViewEmergencies : Page
    {
        EmergencyController emergencyController;
        List<Emergency> emergencies;

        public ViewEmergencies()
        {
            InitializeComponent();
            emergencyController = new EmergencyController();
            emergencies = emergencyController.viewAllEmergencies();
            
            foreach (var e in emergencies)
            {
                EmergencyGrid.Items.Add(e);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmergency());
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var emergency = EmergencyGrid.Items.GetItemAt(EmergencyGrid.SelectedIndex);
            var emergencyID = Convert.ToInt32(emergency.GetType().GetProperty("Id").GetValue(emergency).ToString());
            NavigationService.Navigate(new EmergencyDetails(emergencyID));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var emergency = EmergencyGrid.Items.GetItemAt(EmergencyGrid.SelectedIndex);
            var emergencyID = Convert.ToInt32(emergency.GetType().GetProperty("Id").GetValue(emergency).ToString());
            NavigationService.Navigate(new AddEmergency(emergencyID));
        }
    }


}
