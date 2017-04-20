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
    /// Interaction logic for AddEmergency.xaml
    /// </summary>
    public partial class AddEmergency : Page
    {
        EmergencyController emergencyController;
        Emergency emergency;
        FormValidator emergencyValidator;

        public AddEmergency()
        {
            InitializeComponent();
            emergencyController = new EmergencyController();
            emergency = new Emergency();
            PageTitle.Text = "Give Emergency Aid";
            //This allows for binding with emergency
            //Changes to the form will immediately change the associated emergency attributes
            DataContext = new
            {
                emergency
            };
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            if (isValid())
            {
                emergencyController.addEmergency(emergency);
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                Control control = (FindName(emergencyValidator.ErrorMessages.Keys.ToArray()[0]) as Control);
                control.BringIntoView();

                ValidatorPopupLabel.Content = emergencyValidator.ErrorMessages.Values.ToArray()[0];
                ValidatorPopup.PlacementTarget = control;
                ValidatorPopup.VerticalOffset = 0 - (control.ActualHeight * 2.5);

                showPopup(ValidatorPopup, 2);
            }
        }

        private bool isValid()
        {
            emergencyValidator = new FormValidator(new List<FormsAPI.Validation.Validation>()
            {
                new RequiredValidator(emergency.FirstName, firstName, "First name is a required field."),
                new RequiredValidator(emergency.LastName, lastName, "Last name is a required field."),
                new RequiredValidator(emergency.ItemsReceived, goods, "Goods given is a required field."),
                new RequiredValidator(emergency.DateReceived, dateGiven, "Date is a required field."),
            });

            return emergencyValidator.isValid();
        }

        private void showPopup(Popup popupToShow, double timeout)
        {
            popupToShow.IsOpen = true;

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(timeout)
            };

            timer.Tick += delegate (object sender, EventArgs e)
            {
                ((DispatcherTimer)timer).Stop();
                if (popupToShow.IsOpen) popupToShow.IsOpen = false;
            };

            timer.Start();
        }
    }
}