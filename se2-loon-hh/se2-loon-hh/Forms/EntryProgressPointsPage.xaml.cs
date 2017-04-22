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
using se2_loon_hh.Forms.FormsAPI;
using se2_loon_hh.Forms.FormsAPI.Validation.Validators;
using se2_loon_hh.Forms.FormsAPI.Validation.Form;
using se2_loon_hh.Forms.FormsAPI.Validation;


namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for ProgressPointsPage.xaml
    /// </summary>
    public partial class EntryProgressPointsPage : Page
    {
        ProgressPointsController pointsController;
        ProgressPointTracking Points;
        FormValidator PointsValidator { get; set; }
        bool operation = true;//true is subtract // false is add 
        bool edit = false;
        string msg;
        string tempPoints;


        public EntryProgressPointsPage(int id = 0)
        {
            pointsController = new ProgressPointsController();
            InitializeComponent();
            setupFormElements();
            PageTitle.Text = "Enter Progress Points";
            if (id > 0)
            {
                Points = pointsController.GetPoints(id);
                PageTitle.Text = "Edit Progress Points";
                populateForm();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            // MessageBox.Show(Points.Description + " "+ Points.Date + " " + Points.PointsSpent + " " + Points.Comment);
            populatePoints();
            if (isValid())
            {
                this.Points.PointsSpent = Convert.ToInt64(tempPoints);
                DataContext = PointsValidator.ErrorMessages;
                foreach (var message in PointsValidator.ErrorMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
                savePoints();
                pointsController.calculatePoints((int)this.Points.Id, (int)this.Points.ClientId, operation);
                MessageBox.Show("Record Saved Successfully!");
                this.NavigationService.Navigate(new ViewProgressPointsPage());//go to home page
            }
            else
            {
                DataContext = PointsValidator.ErrorMessages;
                foreach (var message in PointsValidator.ErrorMessages)
                {
                    msg += message.Key + ": " + message.Value + "\n";

                    //Console.WriteLine(message.Key);
                    //Console.WriteLine(message.Value);
                }
                MessageBox.Show(msg);
            }
            msg = "";
        }


        // <summary>
        // This function populates validates from the FormValidator API
        // </summary>
        private bool isValid()
        {
            PointsValidator = new FormValidator(new List<FormsAPI.Validation.Validation>() {
                new RegexValidator(this.Points.ClientId.ToString(), "[^0]+", ClientComboBox, "A client must be selected"),
                new RequiredValidator(this.Points.Date, DatePointsDatePicker, "A date must be selected"),
                new RequiredValidator(this.Points.Description,Description,"A description must be entered" ),
                new RegexValidator(tempPoints,"/^[0-9.]+$/",Points_Spent,"Points must be entered" ),
               //new RequiredValidator(this.Points.PointsSpent.ToString(),Points_Spent,"Points must be entered" ),
               //new RegexValidator(tempPoints, "[0-2000]", Points_Spent, "Range must be from 0-2000")
               //new BaseBoundsValidator(Convert.ToInt64(tempPoints),0, 2000, Points_Spent,"Must be greater than 0 and less than 2000");
            });
            if (!(bool)Increase.IsChecked && !(bool)Decrease.IsChecked)
            {
                this.msg += "Error: Please select Increase or Decrease\n";
            }
            return (PointsValidator.isValid()) ? true : false;
        }

        // <summary>
        // This function populates the ProgressPointsTracking and check if it is valid
        // </summary>
        private void populatePoints()
        {
            if (this.Points == null)
            {
                Points = new ProgressPointTracking();
            }
            else
            {
                this.edit = true;
            }
            this.Points.Description = Description.Text;
            this.Points.Date = DatePointsDatePicker.Text;
            this.tempPoints = Points_Spent.Text;
            //this.Points.PointsSpent = Convert.ToInt64(Points_Spent.Text);
            this.Points.Comment = Comment.Text;
            this.Points.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);
            if ((bool)Increase.IsChecked)
            {
                operation = false;//add points 
            }

            //this.Points.Letter
            //this.Points.NoShow
        }

        // <summary>
        // If edit button is chosen populate the form based on the appropriate client 
        // </summary>
        private void populateForm()
        {
            ClientComboBox.Text = this.Points.Client.FirstName + " " + this.Points.Client.MiddleName + " " + this.Points.Client.LastName;
            Description.Text = this.Points.Description;
            DatePointsDatePicker.Text = this.Points.Date;
            Points_Spent.Text = Convert.ToString(this.Points.PointsSpent);
            Comment.Text = this.Points.Comment;
        }

        /// <summary>
        /// this function calls the EditPoints or the CreatePoints 
        /// method in the pointsController 
        /// </summary>
        private void savePoints()
        {
            if (edit)
            {
                pointsController.EditPoints(this.Points);
            }
            else
            {
                pointsController.CreatePoints(this.Points);
            }
        }

        /// <summary>
        /// This function uses the ComboBoxPairs class to store
        /// each client in the combo box as a key/value.
        /// </summary>
        private void setupFormElements()
        {
            //Console.Write(pointsController.clientList());
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = pointsController.GetClientsForComboBox();//store ComboBoxPairs data structure
        }
    }
}