using se2_loon_hh.Controllers;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        ClientController clientController;
        Client client;
        Pregnancy pregnancy;
        FormValidator ClientValidator;

        public AddClientPage()
        {
            InitializeComponent();

            clientController = new ClientController();

            client = new Client();
            pregnancy = new Pregnancy();

            // This creates a datacontext that allows binding with client and pregnancy
            // Any changes to the form will immediately change the associated client or pregnancy attributes
            DataContext = new
            {
                client,
                pregnancy
            }; 
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                // Add default values for new client
                client.ActiveInactiveStatus = "Active";
                client.ProgressPoints = 0;

                if ((bool)IsPregnant.IsChecked) // Create new client and pregnancy
                {
                    pregnancy.ClientId = client.Id;
                    clientController.addClient(client, pregnancy);
                }
                else                           // Create new client
                {
                    clientController.addClient(client);
                }

                this.NavigationService.Navigate(new MainPage());
            }
            else
            {
                // Show popup at first field that needs validating
                Control control = (FindName(ClientValidator.ErrorMessages.Keys.ToArray()[0]) as Control); // Get first control that needs validating
                control.BringIntoView();                                                                  // Scroll (change page) to control

                ValidatorPopupLabel.Content = ClientValidator.ErrorMessages.Values.ToArray()[0];          // Set content of popup to error message
                ValidatorPopup.PlacementTarget = control;                                                 // Place popup at control
                ValidatorPopup.VerticalOffset = 0 - (control.ActualHeight * 2.5);                         // Offset popup location right above control (2.5 * the height seemed to be a good position)
                showPopup(ValidatorPopup, 2);                                                             // Show the popup for 2 seconds, or until the user makes an action
            }
        }

        private bool isValid()
        {
            ClientValidator = new FormValidator(new List<FormsAPI.Validation.Validation>()
            {
                new RequiredValidator(client.FirstName, FirstNameTextBox, "First Name is Required"),
                new RequiredValidator(client.Ethnicity, EthnicityComboBox, "Ethnicity Selection is Required"),
                new RequiredValidator(client.ZipCode, ZipCodeTextBox, "Zip Code is Required"),
                //new RegexValidator(client.ZipCode, "^\\d{5}$", ZipCodeTextBox, "Zip Code Format is Invalid"), // Since not all the zip codes in the spreadsheet are valid, it might be best not to implement this
                new RequiredValidator(client.MaritalStatus, MaritalStatusComboBox, "Marital Status Selection is Required"),
                new RequiredValidator(client.CurrentStudentEnrollment, CurrentStudentEnrollmentComboBox, "Current Student Enrollment Selection is Required"),
                new RequiredValidator(client.EducationalBackground, EducationalBackgroundComboBox, "Educational Background Selection is Required"),
                new RequiredValidator(client.ApplicationDate, ApplicationDateDatePicker, "Application Date is Required"),
                new RequiredValidator(client.MotherhoodProgram.ToString(), MotherhoodProgramYesRadioButton, "Motherhood Program Selection is Required"),
                new RequiredValidator(client.ParentingProgram.ToString(), ParentingProgramYesRadioButton, "Parenting Program Selection is Required")
            });

            // Only add this rule if creating a pregnancy
            if ((bool)IsPregnant.IsChecked)
            {
                RequiredValidator rule = new RequiredValidator(pregnancy.OBCareBeforeReg.ToString(), OBCareBeforeRegYesRadioButton, "OB Care Before Registration is Required");
                ClientValidator.Validators.Add(rule.FormElement.Name, rule);
            }

            return ClientValidator.isValid();
        }

        private void showPopup(Popup popupToShow, double timeout)
        {
            // This code runs an asynchronous timer that will close the popup in x seconds if the user doesn't close it manually
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
