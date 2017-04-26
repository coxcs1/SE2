using se2_loon_hh.Controllers;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddVolunteerPage.xaml
    /// </summary>
    public partial class AddVolunteerPage : Page
    {
        VolunteerController volunteerController;
        Volunteer volunteer;
        FormValidator VolunteerValidator { get; set; }//this serves as a validator for the entire form

        public AddVolunteerPage(int volunteerID = 0)
        {
            volunteerController = new VolunteerController();
            InitializeComponent();//setup componentss
            PageTitle.Text = "Add Volunteer";//set the page title
            if (volunteerID > 0)
            {
                PageTitle.Text = "Edit Volunteer";//change the page title if it's an edit
                volunteer = volunteerController.GetVolunteer(volunteerID);//fetch the service requested from the database
                populateFormElements();//populate each form element as needed
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageTitle.Text == "Edit Volunteer")
            {
                this.NavigationService.Navigate(new ViewVolunteerPage());
            }

            else
            {
                this.NavigationService.Navigate(new MainPage());
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            if (isValid())
            {
                DataContext = VolunteerValidator.ErrorMessages;
                foreach (var message in VolunteerValidator.ErrorMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
                Console.WriteLine("Form is valid!");
                storeVolunteer();//save the service
            }
            else
            {
                // Show popup at first field that needs validating
                Control control = (FindName(VolunteerValidator.ErrorMessages.Keys.ToArray()[0]) as Control); // Get first control that needs validating
                control.BringIntoView();                                                                  // Scroll (change page) to control

                ValidatorPopupLabel.Content = VolunteerValidator.ErrorMessages.Values.ToArray()[0];          // Set content of popup to error message
                ValidatorPopup.PlacementTarget = control;                                                 // Place popup at control
                ValidatorPopup.VerticalOffset = 0 - (control.ActualHeight * 2.5);                         // Offset popup location right above control (2.5 * the height seemed to be a good position)
                showPopup(ValidatorPopup, 2);
            }
        }

        private bool isValid()
        {
            string hours = Convert.ToString(this.Hours.Text);

            if (hours == "")
            {
                hours = Convert.ToString(0);
            }
            //create validation layer
            VolunteerValidator = new FormValidator(new List<FormsAPI.Validation.Validation>() {
                new BaseBoundsValidator(Convert.ToInt32(hours), 0, 169, Hours, "Volunteer hours cannot be greater than 168 or less than 1."),
                new RegexValidator(this.FirstName.Text, "^[a-zA-Z]+$", FirstName, "Volunteer FirstName must be comprised of only letters."),
                new RegexValidator(this.LastName.Text, "^[a-zA-Z]+$", LastName, "Volunteer LastName must be comprised of only letters."),
                new RequiredValidator(this.Service.Text, Service, "Volunteer Service is required."),
                new RequiredValidator(this.DateReceivedDatePicker.Text, DateReceivedDatePicker, "Volunteer DateReceived is required.")

            });

            return (VolunteerValidator.isValid()) ? true : false;
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

        private void storeVolunteer()
        {

            if (this.volunteer != null)
            {
                //modify the existing volunteer with property calls
                this.volunteer.Hours = Convert.ToInt32(Hours.Text);
                this.volunteer.Service = Service.Text;
                this.volunteer.DateReceived = DateReceivedDatePicker.Text;
                this.volunteer.FirstName = FirstName.Text;
                this.volunteer.LastName = LastName.Text;
                this.volunteer.Comment = Comment.Text;
                volunteerController.EditVolunteer(this.volunteer);//save the new changes to the database
            }

            else
            {

                var volunteer = new Volunteer();

                volunteer.DateReceived = DateReceivedDatePicker.Text;
                volunteer.Hours = Convert.ToInt32(Hours.Text);
                volunteer.Service = Service.Text;
                volunteer.FirstName = FirstName.Text;
                volunteer.LastName = LastName.Text;
                volunteer.Comment = Comment.Text;
                volunteerController.CreateVolunteer(volunteer);//persist the service into the database
            }

            this.NavigationService.Navigate(new ViewVolunteerPage());//go to home page
        }

        /// <summary>
        /// This function populates the form.
        /// </summary>
        private void populateFormElements()
        {
            Hours.Text = Convert.ToString(volunteer.Hours);
            Service.Text = volunteer.Service;
            DateReceivedDatePicker.Text = volunteer.DateReceived;
            FirstName.Text = volunteer.FirstName;
            LastName.Text = volunteer.LastName;
            Comment.Text = volunteer.Comment;

        }
    }
}