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
    /// Interaction logic for AddCleintFreshStartPage.xaml
    /// </summary>
    public partial class AddClientFreshStartPage : Page
    {
        FreshStartController freshStartController;
        FormValidator ClientFreshStartValidator { get; set; }
        ClientFreshStart client;
        bool edit = false;

        public AddClientFreshStartPage(int clientFreshStartID = 0)
        {
            freshStartController = new FreshStartController();//new up our controller
            InitializeComponent();
            PageTitle.Text = "Create Client Fresh Start";
            setupFormElements();//setup the comboboxes

            if (clientFreshStartID > 0)
            {
                PageTitle.Text = "Edit Client Fresh Start";
                this.client = freshStartController.GetClientFreshStart(clientFreshStartID);//fetch the client fresh start from the database
                populateFormElements();//fill up the form with the client fresh start information
            }
        }

        /// <summary>
        /// This function populates the relative object with the form data
        /// and persists the relative or displays error messages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            populateClient();//populate the entity from our form
            //if the form is valid then store the cleint
            //else show some error messages...
            if (isValid())
            {
                storeClient();
            }
            else
            {
                if (!edit)
                {
                    this.client = null;
                }
                DataContext = ClientFreshStartValidator.ErrorMessages;
            }
        }

        /// <summary>
        /// This function checks to see whether the form is valid..
        /// </summary>
        /// <returns></returns>
        private bool isValid()
        {
            ClientFreshStartValidator = new FormValidator(new List<FormsAPI.Validation.Validation>() {
                new RegexValidator(this.client.ClientId.ToString(), "[^0]+", ClientComboBox, "A client must be selected"),
                new RegexValidator(this.client.FreshStartId.ToString(), "[^0]+", FreshStartComboBox, "A fresh start program must be selected"),
                new RequiredValidator(this.client.Date, ClientDate, "Date is required"),
                new RequiredValidator(this.client.Status, ClientStatus, "Status is required")
            });

            return ClientFreshStartValidator.isValid();
        }

        /// <summary>
        /// Set up all combo boxes to contain approriate data...
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            FreshStartComboBox.DisplayMemberPath = "_Value";//what to display
            FreshStartComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            SupportGroupYear.DisplayMemberPath = "_Value";//what to display
            SupportGroupYear.SelectedValuePath = "_Key";//what to use when passing data to back end
            SupportGroupMonth.DisplayMemberPath = "_Value";//what to display
            SupportGroupMonth.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = freshStartController.GetClientsForComboBox();
            FreshStartComboBox.ItemsSource = freshStartController.GetAllFreshStarts();
            SupportGroupYear.ItemsSource = freshStartController.GetYears();
            SupportGroupMonth.ItemsSource = freshStartController.GetMonths();
        }

        /// <summary>
        /// This function populates the client fresh start object
        /// and the support groups for that client.
        /// </summary>
        private void populateClient()
        {
            if (this.client == null)
            {
                this.client = new ClientFreshStart();
            }
            else
            {
                this.edit = true;
            }
            //save client fresh start information
            this.client.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);
            this.client.FreshStartId = Convert.ToInt64(FreshStartComboBox.SelectedValue);
            this.client.Date = ClientDate.Text;
            this.client.Status = ClientStatus.Text;
            //save each support group for that client
            this.client.SupportGroups.Clear();
            foreach (var item in SupportGroupsDataGrid.Items)
            {
                var supportGroup = new SupportGroup();
                supportGroup.ClientFreshStart = this.client;
                supportGroup.MonthAttended = item.GetType().GetProperty("Month").GetValue(item).ToString();
                supportGroup.Year = Convert.ToInt64(item.GetType().GetProperty("Year").GetValue(item).ToString());
                this.client.SupportGroups.Add(supportGroup);
            }
        }
        /// <summary>
        /// This function persists the client fresh start entity into the database..
        /// </summary>
        private void storeClient()
        {
            if (edit)
            {
                freshStartController.EditClientFreshStart(this.client);
            }
            else
            {
                freshStartController.CreateClientFreshStart(this.client);
            }
            this.NavigationService.Navigate(new MainPage());
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
        /// This function adds a row to the support group data grid..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSupportGroupButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Month = SupportGroupMonth.Text, Year = SupportGroupYear.Text };
            SupportGroupsDataGrid.Items.Add(row);
        }
        /// <summary>
        /// This function populates the form with all relevant data
        /// </summary>
        private void populateFormElements()
        {
            ClientComboBox.Text = this.client.Client.FirstName + " " + this.client.Client.MiddleName + " " + this.client.Client.LastName;
            FreshStartComboBox.Text = this.client.FreshStart.Name;
            ClientDate.Text = this.client.Date;
            ClientStatus.Text = this.client.Status;
            //populate the data grid...
            foreach (var supportGroup in this.client.SupportGroups)
            {
                SupportGroupsDataGrid.Items.Add(new {
                    Month = supportGroup.MonthAttended,
                    Year = Convert.ToString(supportGroup.Year)
                });
            }
        }

        /// <summary>
        /// This function removes a support group from the data grid..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSupportGroup_Click(object sender, RoutedEventArgs e)
        {
            SupportGroupsDataGrid.Items.RemoveAt(SupportGroupsDataGrid.SelectedIndex);
        }
    }
}
