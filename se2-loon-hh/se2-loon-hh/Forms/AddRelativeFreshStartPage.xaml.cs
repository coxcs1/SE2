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
        RelativeFreshStart relative;
        FormValidator RelativeFormValidator { get; set; }
        bool edit = false;

        public AddRelativeFreshStartPage(int relativeID = 0)
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
            setupFormElements();
            //if there is an edit request then change the title and fetch the relative and populate the form..
            if (relativeID > 0)
            {
                PageTitle.Text = "Edit Relative";
                this.relative = freshStartController.GetRelative(relativeID);
                populateForm();
            }
        }
        /// <summary>
        /// This function populates all combo boxes for the add relatives page...
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            FreshStartComboBox.DisplayMemberPath = "_Value";//what to display
            FreshStartComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = freshStartController.GetClientsForComboBox();
            FreshStartComboBox.ItemsSource = freshStartController.GetAllFreshStarts();
        }
        /// <summary>
        /// This function redirects the user back to the main page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
        /// <summary>
        /// This function populates the relative object with the form data
        /// and persists the relative or displays error messages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            populateRelative();

            if (isValid())
            {
                storeRelative();
            }
            else
            {
                if (!edit)
                {
                    this.relative = null;
                }
                DataContext = RelativeFormValidator.ErrorMessages;
            }
        }
        /// <summary>
        /// This function checks to see whether the form is valid and then
        /// displays error message appropriately..
        /// </summary>
        /// <returns></returns>
        private bool isValid()
        {
            RelativeFormValidator = new FormValidator(new List<FormsAPI.Validation.Validation>(){
                new RegexValidator(this.relative.ClientId.ToString(), "[^0]+", ClientComboBox, "A client must be selected"),
                new RegexValidator(this.relative.FreshStartId.ToString(), "[^0]+", FreshStartComboBox, "A fresh start program must be selected"),
                new RequiredValidator(this.relative.FirstName, RelativeFirstName, "First name is required"),
                new RequiredValidator(this.relative.LastName, RelativeLastName, "Last name is required"),
                new RequiredValidator(this.relative.Date, RelativeDate, "Date is required"),
                new RequiredValidator(this.relative.Status, RelativeStatus, "Status is required"),
                new RequiredValidator(this.relative.RelationshipTo, RelativeRelationshipTo, "Relationship is required"),
            });

            return (RelativeFormValidator.isValid()) ? true : false;
        }

        /// <summary>
        /// This function populates the realtive fresh start entity from the form.
        /// </summary>
        private void populateRelative()
        {
            if (this.relative == null)
            {
                this.relative = new RelativeFreshStart();
            }
            else
            {
                this.edit = true;
            }
            //populate relative fresh start entity..
            this.relative.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);
            this.relative.FreshStartId = Convert.ToInt64(FreshStartComboBox.SelectedValue);
            this.relative.FirstName = RelativeFirstName.Text;
            this.relative.LastName = RelativeLastName.Text;
            this.relative.Date = RelativeDate.Text;
            this.relative.Status = RelativeStatus.Text;
            this.relative.RelationshipTo = RelativeRelationshipTo.Text;
        }
        /// <summary>
        /// This function calls on the controller to persist the relative entity..
        /// </summary>
        private void storeRelative()
        {
            if (!edit)
            {
                freshStartController.saveRelative(this.relative);
            }
            else
            {
                freshStartController.editRelative(this.relative);
            }
            this.NavigationService.Navigate(new MainPage());
        }
        /// <summary>
        /// This function populates the form from a relative fresh start in the database..
        /// </summary>
        private void populateForm()
        {
            ClientComboBox.Text = this.relative.Client.FirstName + " " + this.relative.Client.MiddleName + " " + this.relative.Client.LastName;
            FreshStartComboBox.Text = this.relative.FreshStart.Name;
            RelativeFirstName.Text = this.relative.FirstName;
            RelativeLastName.Text = this.relative.LastName;
            RelativeDate.Text = this.relative.Date;
            RelativeStatus.Text = this.relative.Status;
            RelativeRelationshipTo.Text = this.relative.RelationshipTo;
        }
    }
}
