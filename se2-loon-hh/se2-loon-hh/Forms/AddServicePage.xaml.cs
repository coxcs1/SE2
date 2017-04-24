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
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        ServiceController serviceController;//Handles all persistence layer logic for services and donations
        ServiceInfo service;//if the form is receiving an ID then populate this attribute
        FormValidator ServiceValidator { get; set; }//this serves as a validator for the entire form
        bool edit = false;//whether or not the request is to edit a service (Default false)


        public AddServicePage(int serviceRequestedID = 0)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            setupFormElements();//populate the form
            PageTitle.Text = "Add Service Info";//set the page title
            if (serviceRequestedID > 0)
            {
                PageTitle.Text = "Edit Service Info";//change the page title if it's an edit
                service = serviceController.GetServiceInfo(serviceRequestedID);//fetch the service requested from the database
                populateFormElements();//populate each form element as needed
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());//go to home page
        }
        /// <summary>
        /// Checks to see if the service requested is null or not and
        /// either updates or inserts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            populateService();//fill up the service requested attribute
            if (isValid())
            {
                DataContext = ServiceValidator.ErrorMessages;
                foreach (var message in ServiceValidator.ErrorMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
                Console.WriteLine("Form is valid!");
                storeService();//save the service
                this.NavigationService.Navigate(new ViewServicesPage());//go to home page
            }
            else
            {
                DataContext = ServiceValidator.ErrorMessages;
                //if adding the service didn't work then reset it
                //because it will try persist under the edit function.
                if (!edit)
                {
                    this.service = null;
                }
            }
        }

        private bool isValid()
        {
            //create validation layer
            ServiceValidator = new FormValidator(new List<FormsAPI.Validation.Validation>() {
                new RegexValidator(this.service.ClientId.ToString(), "[^0]+", ClientComboBox, "A client must be selected"),
                new RequiredValidator(this.service.Date, DateArrivedDatePicker, "A date must be selected")
            });
            

            return (ServiceValidator.isValid()) ? true : false;
        }

        /// <summary>
        /// This function populates the service requested attribute.
        /// </summary>
        private void populateService()
        {
            //if the service info is null then create a new on
            //else the service is being edited
            if (this.service == null)
            {
                this.service = new ServiceInfo();
            }
            else
            {
                this.edit = true;
            }

            //populate the service attribute
            this.service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            this.service.NewWalkIn = Convert.ToInt64(NewWalkIn.IsChecked);
            this.service.WalkIn = Convert.ToInt64(WalkIn.IsChecked);
            this.service.PhoneAfterHrs = Convert.ToInt64(TelephoneAfterHours.IsChecked);
            this.service.PrankCall = Convert.ToInt64(PrankCall.IsChecked);
            this.service.OffSite = Convert.ToInt64(OffSite.IsChecked);
            this.service.RepBySomeoneElse = Convert.ToInt64(RepresentedBySomeoneElse.IsChecked);
            this.service.OutgoingCallMailEmail = Convert.ToInt64(OutgoingCallMailEmail.IsChecked);
            this.service.Email = Convert.ToInt64(Email.IsChecked);
            this.service.NewContact = Convert.ToInt64(NewContact.IsChecked);
            this.service.PhoneCenterHrs = Convert.ToInt64(PhoneCenterHrs.IsChecked);
            this.service.Date = DateArrivedDatePicker.Text;
            this.service.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);
            //clear both items and services requested
            this.service.ItemRequesteds.Clear();
            this.service.ServiceRequesteds.Clear();
            //populate the items requested list
            foreach (var item in ItemsRequestedDataGrid.Items)
            {
                var itemRequested = new ItemRequested();
                itemRequested.ItemName = item.GetType().GetProperty("Name").GetValue(item).ToString();
                itemRequested.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                this.service.ItemRequesteds.Add(itemRequested);
            }
            //populate the services requested list
            foreach (var item in ServiceRequestedDataGrid.Items)
            {
                var serviceRequested = new ServiceRequested();
                serviceRequested.ServiceName = item.GetType().GetProperty("Name").GetValue(item).ToString();
                serviceRequested.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                this.service.ServiceRequesteds.Add(serviceRequested);
            }
        }
        /// <summary>
        /// This function persists the service requested object into the database.
        /// </summary>
        private void storeService()
        {
            //call approriate controller action
            if (edit)
            {
                serviceController.EditService(this.service);
            }
            else
            {
                serviceController.CreateService(this.service);
            }
        }

        /// <summary>
        /// This function uses the ComboBoxPairs class to store
        /// each client in the combo box as a key/value.
        /// </summary>
        private void setupFormElements()
        {
            ClientComboBox.DisplayMemberPath = "_Value";//what to display
            ClientComboBox.SelectedValuePath = "_Key";//what to use when passing data to back end
            ClientComboBox.ItemsSource = serviceController.GetClientsForComboBox();//store ComboBoxPairs data structure
        }   
        /// <summary>
        /// This function adds a row to the dontations data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemRequestedButton_Click(object sender, RoutedEventArgs e)
        {
           var row = new { Name = ItemRequestedName.Text, Comment = ItemRequestedComment.Text };
            ItemsRequestedDataGrid.Items.Add(row);
        }
        /// <summary>
        /// This function removes a row from the donations data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveItemRequested_Click(object sender, RoutedEventArgs e)
        {
            ItemsRequestedDataGrid.Items.RemoveAt(ItemsRequestedDataGrid.SelectedIndex);
        }
        /// <summary>
        /// This function populates the form.
        /// </summary>
        private void populateFormElements()
        {
            ClientComboBox.Text = this.service.Client.FirstName + " " + this.service.Client.MiddleName + " " + this.service.Client.LastName;
            DateArrivedDatePicker.Text = this.service.Date;
            NewContact.IsChecked = Convert.ToBoolean(this.service.NewContact);
            WalkIn.IsChecked = Convert.ToBoolean(this.service.WalkIn);
            NewWalkIn.IsChecked = Convert.ToBoolean(this.service.NewWalkIn);
            TelephoneAfterHours.IsChecked = Convert.ToBoolean(this.service.PhoneAfterHrs);
            PrankCall.IsChecked = Convert.ToBoolean(this.service.PrankCall);
            OffSite.IsChecked = Convert.ToBoolean(this.service.OffSite);
            RepresentedBySomeoneElse.IsChecked = Convert.ToBoolean(this.service.RepBySomeoneElse);
            OutgoingCallMailEmail.IsChecked = Convert.ToBoolean(this.service.OutgoingCallMailEmail);
            Email.IsChecked = Convert.ToBoolean(this.service.Email);
            PhoneCenterHrs.IsChecked = Convert.ToBoolean(this.service.PhoneCenterHrs);
            //populate the items requested data grid
            foreach (var item in this.service.ItemRequesteds)
            {
                ItemsRequestedDataGrid.Items.Add(new { Name = item.ItemName, Comment = item.Comment });
            }
            //populate the services requested data grid
            foreach (var serviceRequested in this.service.ServiceRequesteds)
            {
                ServiceRequestedDataGrid.Items.Add(new { Name = serviceRequested.ServiceName, Comment = serviceRequested.Comment });
            }
        }
        /// <summary>
        /// This function adds a requested service to the requested services data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddServiceRequestedButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Name = ServiceRequestedName.Text, Comment = ServiceRequestedComment.Text };
            ServiceRequestedDataGrid.Items.Add(row);
        }
        /// <summary>
        /// This function removes a requested service from the requested services data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveServiceRequested_Click(object sender, RoutedEventArgs e)
        {
            ServiceRequestedDataGrid.Items.RemoveAt(ServiceRequestedDataGrid.SelectedIndex);
        }
    }
}
