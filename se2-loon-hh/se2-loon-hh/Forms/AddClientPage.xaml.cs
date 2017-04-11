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

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        ClientController clientController;
        private int childCounter;

        public AddClientPage()
        {
            clientController = new ClientController();
            InitializeComponent();
            childCounter = 0;
            this.DataContext = this; // This is for allowing the height and width of the page to be bound to the window height and width
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Placeholder Text");
        }

        private void AddChildButton_Click(object sender, RoutedEventArgs e)
        {
            childCounter++;

            GroupBox groupbox = new GroupBox(); // Surrounds all elements for child
            groupbox.Header = "Child " + childCounter;
            StackPanel childPanel = new StackPanel(); // Layout for elements

            Grid fnameGrid = new Grid(); // Layout for fname elements
            Label fnameLabel = new Label();
            fnameLabel.Content = "First Name:";
            TextBox fnameTextbox = new TextBox();

            Grid lnameGrid = new Grid(); // Layout for lname elements
            Label lnameLabel = new Label();
            lnameLabel.Content = "Last Name:";
            TextBox lnameTextbox = new TextBox();

            Grid bdayGrid = new Grid(); // Layout for bday elements
            Label bdayLabel = new Label();
            bdayLabel.Content = "Birthdate:";
            DatePicker bdayPicker = new DatePicker();

            Grid custodyGrid = new Grid(); // Layout for custody elements
            Label custodyLabel = new Label();
            custodyLabel.Content = "Custody:";
            StackPanel custodyChoicesLayout = new StackPanel(); // Layout for yes/no radio buttons
            custodyChoicesLayout.HorizontalAlignment = HorizontalAlignment.Right;
            custodyChoicesLayout.Orientation = Orientation.Horizontal;
            RadioButton yesButton = new RadioButton();
            yesButton.Content = "Yes";
            RadioButton noButton = new RadioButton();
            noButton.Content = "No";

            Grid removeChildGrid = new Grid(); // Layout for child removal
            Button removeChildButton = new Button();
            removeChildButton.Tag = childCounter;
            removeChildButton.Content = "Remove";
            removeChildButton.HorizontalAlignment = HorizontalAlignment.Right;
            removeChildButton.Click += RemoveChildButton_Click;

            // Put all elements in corresponding layouts
            removeChildGrid.Children.Add(removeChildButton);
            custodyChoicesLayout.Children.Add(yesButton);
            custodyChoicesLayout.Children.Add(noButton);
            custodyGrid.Children.Add(custodyLabel);
            custodyGrid.Children.Add(custodyChoicesLayout);
            bdayGrid.Children.Add(bdayLabel);
            bdayGrid.Children.Add(bdayPicker);
            lnameGrid.Children.Add(lnameLabel);
            lnameGrid.Children.Add(lnameTextbox);
            fnameGrid.Children.Add(fnameLabel);
            fnameGrid.Children.Add(fnameTextbox);
            childPanel.Children.Add(fnameGrid);
            childPanel.Children.Add(lnameGrid);
            childPanel.Children.Add(bdayGrid);
            childPanel.Children.Add(custodyGrid);
            childPanel.Children.Add(removeChildGrid);
            groupbox.Content = childPanel;
            ChildContainer.Children.Insert(childCounter - 1, groupbox); // Inserts after last child, but before AddChildButton
        }

        private void RemoveChildButton_Click(object sender, RoutedEventArgs e)
        {
            ChildContainer.Children.RemoveAt((int)(e.Source as Button).Tag - 1); // Remove child at index based on Tag on button
            childCounter--; // Decrement child counter

            for(int i = 0; i < childCounter; i++) // Update headers and tags to new indexes
            {
                (ChildContainer.Children[i] as GroupBox).Header = "Child " + (i + 1);
                ((((ChildContainer.Children[i] as GroupBox).Content as StackPanel).Children[4] as Grid).Children[0] as Button).Tag = (i + 1);
            }
        }
    }
}
