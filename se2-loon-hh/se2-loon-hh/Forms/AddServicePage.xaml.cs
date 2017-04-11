﻿using System;
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

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        ServiceController serviceController;
        ServiceRequested serviceRequested;//if the form is receiving an ID then populate this attribute
        
        public AddServicePage(int serviceRequestedID = 0)
        {
            serviceController = new ServiceController();//initialize controller
            InitializeComponent();//setup components
            setupFormElements();//populate the form
            PageTitle.Text = "Add Service";//set the page title
            if (serviceRequestedID > 0)
            {
                PageTitle.Text = "Edit Service";//change the page title if it's an edit
                serviceRequested = serviceController.GetServiceRequested(serviceRequestedID);//fetch the service requested from the database
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
            if (this.serviceRequested != null)
            {
                //modify the existing service with more hidious property calls...
                this.serviceRequested.Service.Name = ServiceName.Text;
                this.serviceRequested.Service.Type = ServiceType.Text;
                this.serviceRequested.Service.Description = ServiceDescription.Text;
                this.serviceRequested.Service.DateArrived = DateArrivedDatePicker.Text;
                this.serviceRequested.Service.NewContact = Convert.ToInt64(NewContact.IsChecked);
                this.serviceRequested.Service.NewWalkIn = Convert.ToInt64(NewWalkIn.IsChecked);
                this.serviceRequested.Service.WalkIn = Convert.ToInt64(WalkIn.IsChecked);
                this.serviceRequested.Service.TelephoneAfterHrs = Convert.ToInt64(TelephoneAfterHours.IsChecked);
                this.serviceRequested.Service.PrankCall = Convert.ToInt64(PrankCall.IsChecked);
                this.serviceRequested.Service.OffSite = Convert.ToInt64(OffSite.IsChecked);
                this.serviceRequested.Service.RepresentedBySomeoneElse = Convert.ToInt64(RepresentedBySomeoneElse.IsChecked);
                this.serviceRequested.Service.OutgoingCallMailEmail = Convert.ToInt64(OutgoingCallMailEmail.IsChecked);
                this.serviceRequested.Service.Email = Convert.ToInt64(Email.IsChecked);
                this.serviceRequested.Service.NewContact = Convert.ToInt64(NewContact.IsChecked);
                this.serviceRequested.Service.Donations.Clear();//clear the list of donations
                //create as many donations as needed and link them to a service
                foreach (var item in DonationsDataGrid.Items)
                {
                    //create the donation
                    var donation = new Donation();
                    donation.Name = item.GetType().GetProperty("Name").GetValue(item).ToString();
                    donation.Type = item.GetType().GetProperty("Type").GetValue(item).ToString();
                    donation.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                    //add the donation to the service
                    this.serviceRequested.Service.Donations.Add(donation);
                }
                serviceController.EditService(this.serviceRequested);//save the new changes to the database
            }
            else
            {
                //create and populate the service using manually ugly property calls
                var service = new Service();
                service.Name = ServiceName.Text;
                service.Type = ServiceType.Text;
                service.Description = ServiceDescription.Text;
                service.DateArrived = DateArrivedDatePicker.Text;
                service.NewContact = Convert.ToInt64(NewContact.IsChecked);
                service.NewWalkIn = Convert.ToInt64(NewWalkIn.IsChecked);
                service.WalkIn = Convert.ToInt64(WalkIn.IsChecked);
                service.TelephoneAfterHrs = Convert.ToInt64(TelephoneAfterHours.IsChecked);
                service.PrankCall = Convert.ToInt64(PrankCall.IsChecked);
                service.OffSite = Convert.ToInt64(OffSite.IsChecked);
                service.RepresentedBySomeoneElse = Convert.ToInt64(RepresentedBySomeoneElse.IsChecked);
                service.OutgoingCallMailEmail = Convert.ToInt64(OutgoingCallMailEmail.IsChecked);
                service.Email = Convert.ToInt64(Email.IsChecked);
                service.NewContact = Convert.ToInt64(NewContact.IsChecked);
                //create as many donations as needed and link them to a service
                foreach (var item in DonationsDataGrid.Items)
                {
                    //create the donation
                    var donation = new Donation();
                    donation.Name = item.GetType().GetProperty("Name").GetValue(item).ToString();
                    donation.Type = item.GetType().GetProperty("Type").GetValue(item).ToString();
                    donation.Comment = item.GetType().GetProperty("Comment").GetValue(item).ToString();
                    //add the donation to the service
                    service.Donations.Add(donation);
                }
                //create the requested service and pass it to the controller
                var sr = new ServiceRequested();
                sr.ClientId = Convert.ToInt64(ClientComboBox.SelectedValue);//attach the client to the requested service
                sr.Service = service;
                sr.DateReceived = DateArrivedDatePicker.Text;
                serviceController.CreateService(service, sr);//persist the service into the database
            }
            this.NavigationService.Navigate(new ViewServicesPage());//go to home page
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
            DonationType.DisplayMemberPath = "_Value";//what to display
            DonationType.SelectedValuePath = "_Key";//what to use when passing data to back end
            DonationType.ItemsSource = serviceController.GetDonationTypes();//load up donation types for the donation type combo box
        }
        /// <summary>
        /// This function adds a row to the dontations data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDonationButton_Click(object sender, RoutedEventArgs e)
        {
            var row = new { Name = DonationName.Text, Type = DonationType.SelectedItem.ToString(), Comment = DonationComment.Text };
            DonationsDataGrid.Items.Add(row);
        }
        /// <summary>
        /// This function removes a row from the donations data grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveDonation_Click(object sender, RoutedEventArgs e)
        {
            DonationsDataGrid.Items.RemoveAt(DonationsDataGrid.SelectedIndex);
        }
        /// <summary>
        /// This function populates the form.
        /// </summary>
        private void populateFormElements()
        {
            ServiceName.Text = serviceRequested.Service.Name;
            ServiceType.Text = serviceRequested.Service.Type;
            ServiceDescription.Text = serviceRequested.Service.Description;
            ClientComboBox.Text = serviceRequested.Client.FirstName + " " + serviceRequested.Client.MiddleName + " " + serviceRequested.Client.LastName;
            DateArrivedDatePicker.Text = serviceRequested.Service.DateArrived;
            NewContact.IsChecked = Convert.ToBoolean(serviceRequested.Service.NewContact);
            WalkIn.IsChecked = Convert.ToBoolean(serviceRequested.Service.WalkIn);
            NewWalkIn.IsChecked = Convert.ToBoolean(serviceRequested.Service.NewWalkIn);
            TelephoneAfterHours.IsChecked = Convert.ToBoolean(serviceRequested.Service.TelephoneAfterHrs);
            PrankCall.IsChecked = Convert.ToBoolean(serviceRequested.Service.PrankCall);
            OffSite.IsChecked = Convert.ToBoolean(serviceRequested.Service.OffSite);
            RepresentedBySomeoneElse.IsChecked = Convert.ToBoolean(serviceRequested.Service.RepresentedBySomeoneElse);
            OutgoingCallMailEmail.IsChecked = Convert.ToBoolean(serviceRequested.Service.OutgoingCallMailEmail);
            Email.IsChecked = Convert.ToBoolean(serviceRequested.Service.Email);
            //populate the donation data grid
            foreach (var donation in serviceRequested.Service.Donations)
            {
                DonationsDataGrid.Items.Add(new { Name = donation.Name, Type = donation.Type, Comment = donation.Comment });
            }
        }
    }
}
