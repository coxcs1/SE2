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
    /// Interaction logic for AddFreshStart.xaml
    /// </summary>
    public partial class AddFreshStart : Page
    {
        FreshStartController freshStartController;
        FreshStart freshStart;
        FormValidator FreshStartValidator { get; set; }
        bool edit = false;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="freshStartID"></param>
        public AddFreshStart(int freshStartID = 0)
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
            setupFormElements();//populate the type combo box
            //if there is an id then fetch it and populate the form
            if (freshStartID > 0)
            {
                freshStart = freshStartController.GetFreshStart(freshStartID);
                populateFormElements();
            }
        }
        /// <summary>
        /// This function populates the fresh start attribute and checks
        /// to see if the form is valid.
        /// If so, then the fresh start is persisted into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            populateFreshStart();//populate object from form...
            //if valid save, else show error messages.
            if (isValid())
            {
                storeFreshStart();
            }
            else
            {
                DataContext = FreshStartValidator.ErrorMessages;//display the appropriate error messages
                //reset the object because it will try to edit...
                if (!edit)
                {
                    this.freshStart = null;//set the object back to null...
                }
            }
        }
        /// <summary>
        /// This function checks to see if the fresh start attribute is initialized
        /// and then populates the object.
        /// </summary>
        private void populateFreshStart()
        {
            if (this.freshStart == null)
            {
                this.freshStart = new FreshStart();
            }
            else
            {
                edit = true;
            }
            
            this.freshStart.Name = FreshStartName.Text;
            this.freshStart.CurriculumType = FreshStartType.Text;
            this.freshStart.Comment = FreshStartComment.Text;
        }
        /// <summary>
        /// This function checks to see whether the form is valid...
        /// </summary>
        /// <returns>Whether or not the form is valid..</returns>
        private bool isValid()
        {
            FreshStartValidator = new FormValidator(new List<FormsAPI.Validation.Validation>() {
                new RequiredValidator(this.freshStart.Name, FreshStartName, "A fresh start name is required."),
                new RegexValidator(this.freshStart.CurriculumType, "[^0]+", FreshStartType, "A fresh start type is required"),
            });

            return (FreshStartValidator.isValid()) ? true : false;
        }
        /// <summary>
        /// This function fills form elements with fresh start data.
        /// </summary>
        private void populateFormElements()
        {
            FreshStartName.Text = this.freshStart.Name;
            FreshStartType.Text = this.freshStart.CurriculumType;
            FreshStartComment.Text = this.freshStart.Comment;
        }
        /// <summary>
        /// This function populates the types combo box.
        /// </summary>
        private void setupFormElements()
        {
            FreshStartType.ItemsSource = freshStartController.GetFreshStartTypes();
        }
        /// <summary>
        /// This function persists the fresh start object into the database and redirects to main page.
        /// </summary>
        public void storeFreshStart()
        {
            if (!edit)
            {
                freshStartController.saveFreshStart(this.freshStart);
            }
            else
            {
                freshStartController.editFreshStart(this.freshStart);
            }
            this.NavigationService.Navigate(new MainPage());
        }
        /// <summary>
        /// This function takes the user back to the home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
