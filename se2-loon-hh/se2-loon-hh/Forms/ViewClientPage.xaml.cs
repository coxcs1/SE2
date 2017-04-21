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


namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for ViewClientPage.xaml
    /// </summary>
    public partial class ViewClientPage : Page
    {
        private int id;
        private ClientController clientController;

        public ViewClientPage(int clientId)
        {
            InitializeComponent();

            clientController = new ClientController();
            id = clientId;
            Client c = clientController.GetSingleClient(clientId);

            ClientLabel.Content = c.FirstName + " " + c.LastName + ", DOB: " + c.BirthDate;

            SetupGeneral(c);
            SetupPregnancy(c);
            SetUpCourseAttendance(c);

            //initializes button text and tag -> sets up the view for the current year
            SupportGroupYearShowing.Content = "Year Showing: " + DateTime.Now.Year;
            ChangeYearViewSG.Content = "View Previous Years";
            ChangeYearViewSG.Tag = "Current";
            SetSuppotGroupsCurrentYear(c);

            //points tab setup
            CurrentPointsLabel.Content = "Current Points Amount: " + c.ProgressPoints.ToString();
            PointsList.ItemsSource = clientController.GetProgressPointsList(c);

            SetupServiceTab(c);                                   
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClientListPage());
        }


        #region General Tab
        private void SetupGeneral(Client c)
        {
            string name = c.FirstName;
            if (c.MiddleName != null)
                name += " " + c.MiddleName;
            name += " " + c.LastName;

            GeneralTextBlock.Text = "General Information\n";
            GeneralTextBlock.Text += "• Name: " + name + "\n";
            GeneralTextBlock.Text += "• Birth Date: " + c.BirthDate + " (" + clientController.GetClientAge(c) + ")" + "\n";
            GeneralTextBlock.Text += "• Zip Code: " + c.ZipCode + "\n";
            GeneralTextBlock.Text += "• Ethnicity: " + c.Ethnicity + "\n";
            GeneralTextBlock.Text += "• Marital Status: " + c.MaritalStatus + "\n";
            GeneralTextBlock.Text += "• Current Student Enrollment: " + c.CurrentStudentEnrollment + "\n";
            GeneralTextBlock.Text += "• Educational Background: " + c.EducationalBackground + "\n";
            GeneralTextBlock.Text += "• Smoker: " + c.Smoker + "\n";

            if(c.Comment != null)
                GeneralTextBlock.Text += "• Comment: " + c.Comment;

            
            HHTextBlock.Text = "Hope House Information\n";
            HHTextBlock.Text += "• Application Date: " + c.ApplicationDate + "\n";
            HHTextBlock.Text += "• Status: " + c.CurrentStatus + " (" + c.ActiveInactiveStatus + ")" + "\n";
            HHTextBlock.Text += "• Registering Individual: " + c.RegisteringIndividual + "\n";
            HHTextBlock.Text += "• Pregnancy History: " + c.PregnancyHistory + "\n";

            if (c.ContinuedFromPrevYears == 1)
                HHTextBlock.Text += "• Continued From Previous Year\n";
            else if(c.NewThisYear == 1)
                HHTextBlock.Text += "• New this Year\n";


            if (c.MotherhoodProgram == 1)
                HHTextBlock.Text += "• Takes part in Motherhood program\n";
            if (c.ParentingProgram == 1)
                HHTextBlock.Text += "• Takes part in Parenting program\n";

            if (c.NoActivityNewYear == 1)
                HHTextBlock.Text += "• No Activity in New Year\n";
            if (c.BecameInactive == 1)
                HHTextBlock.Text += "• Became Inactive\n";
            if (c.LeftProgramBeforeBirth == 1)
                HHTextBlock.Text += "• Left Program Before Birth\n";
            if (c.StillActiveYE == 1)
                HHTextBlock.Text += "• Still Active\n";
            if (c.ReturnNewPregnancy == 1)
                HHTextBlock.Text += "• Return - New Pregnancy\n";
            if (c.GracePeriod != null)
                HHTextBlock.Text += "• Grace Period: " + c.GracePeriod;
      
        }
        #endregion

        #region Pregnancy Tab
        private void SetupPregnancy(Client c)
        {
            foreach (Pregnancy p in c.Pregnancies)
            {
                string obCare = "";
                if (p.OBCareBeforeReg == 1)
                    obCare = "Before Reg.";
                if (p.OBCareAfterReg == 1)
                    obCare = "After Reg.";

                string continuedWithProgram = "";
                if (p.ContinuedWithMotherhoodProgram == 1)
                    continuedWithProgram = "Yes";
                else if (p.ContinuedWithMotherhoodProgram == 0)
                    continuedWithProgram = "No";

                //for use in the data grid
                PregnancyList.Items.Add(new
                {
                    DueDate = p.DueDate,
                    ObCare = obCare,
                    HealthyDelivery = p.HealthyDelivery,
                    BirthComplications = p.BirthComplications,
                    Continued = continuedWithProgram,
                    Comment = p.Comment
                });

            }
        }
        #endregion

        #region Course Attendance Tab

        private void SetUpCourseAttendance(Client c)
        {
            List<List<ClientFreshStart>> classLists = clientController.GetCoursesTaken(c);
            PrenatalList.ItemsSource = classLists[0];
            MotherhoodList.ItemsSource = classLists[1];
            ElectivesList.ItemsSource = classLists[2];
            RelativesList.ItemsSource = clientController.GetRelativeCoursesTaken(c);

            List<ClassAttendance> sortedByYear = c.ClassAttendances.OrderBy(x => x.Year).Reverse().ToList();
            foreach(ClassAttendance att in sortedByYear)
            {
                ClassAttendanceList.Items.Add(new 
                {
                    Date = att.Month + ", " + att.Year,
                    Count = att.Count,
                    Comment = att.Comment
            });
            }
        }

        #endregion

        #region Support Group Tab
        private void ChangeYearViewSG_Click(object sender, RoutedEventArgs e)
        {
            //grabs the tag from the button
            Button b = ((Button)e.OriginalSource);
            string currentView = b.Tag.ToString();

            Client c = clientController.GetSingleClient(id);

            //if they have clicked the button while on the current view -> switch to previous year view
            if(currentView == "Current")
            {
                //gets the min and max years - min in [0], max in [1]
                int[] minMaxYears = clientController.GetMinMaxYearsForPreviousSG(c);

                //changes the label text to account for only one year, no data, or a range of years
                if (minMaxYears[0] == minMaxYears[1])
                    SupportGroupYearShowing.Content = "Year(s) Showing: " + minMaxYears[0];
                else if(minMaxYears[0] == DateTime.Now.Year)
                    SupportGroupYearShowing.Content = "No Data From Previous Years";
                else
                    SupportGroupYearShowing.Content = "Year(s) Showing: " + minMaxYears[0] + " - " + minMaxYears[1];

                //changes the text and tag for the button
                ChangeYearViewSG.Content = "View Current Year";
                ChangeYearViewSG.Tag = "Previous";

                //sets the text to be displayed
                SetSupportGroupsPreviousYears(c);
            }
            //clicked the button on the previous year view
            else
            {
                //changes the label to show the current year
                SupportGroupYearShowing.Content = "Year Showing: " + DateTime.Now.Year;

                //changes the button text and tag
                ChangeYearViewSG.Content = "View Previous Years";
                ChangeYearViewSG.Tag = "Current";

                //sets the text to be displayed
                SetSuppotGroupsCurrentYear(c);
            }
        }

        private void SetSuppotGroupsCurrentYear(Client c)
        {
            int[] counts = clientController.GetSupportGroupCountsForCurrentYear(c);
            SGMonths.Text = "January: " + counts[0] + "\n" +
                "February: " + counts[1] + "\n" +
                "March: " + counts[2] + "\n" +
                "April: " + counts[3] + "\n" +
                "May: " + counts[4] + "\n" +
                "June: " + counts[5] + "\n" +
                "July: " + counts[6] + "\n" +
                "August: " + counts[7] + "\n" +
                "September: " + counts[8] + "\n" +
                "October: " + counts[9] + "\n" +
                "November: " + counts[10] + "\n" +
                "December: " + counts[11] + "\n";
        }

        private void SetSupportGroupsPreviousYears(Client c)
        {
            int[] counts = clientController.GetSupportGroupCountsForPreviousYears(c);
            SGMonths.Text = "January: " + counts[0] + "\n" +
                "February: " + counts[1] + "\n" +
                "March: " + counts[2] + "\n" +
                "April: " + counts[3] + "\n" +
                "May: " + counts[4] + "\n" +
                "June: " + counts[5] + "\n" +
                "July: " + counts[6] + "\n" +
                "August: " + counts[7] + "\n" +
                "September: " + counts[8] + "\n" +
                "October: " + counts[9] + "\n" +
                "November: " + counts[10] + "\n" +
                "December: " + counts[11] + "\n";
        }
        #endregion

        private void MakePointTransaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Points page");
        }

        #region Service Tab
        private void SetupServiceTab(Client c)
        {
            foreach(ServiceInfo s in c.ServiceInfoes)
            {   
                //groups all contact types so it can be displayed with a single string      
                List<string> contactTypes = new List<string>();
                if (s.WalkIn == 1)
                    contactTypes.Add("Walk In");
                if (s.PhoneCenterHrs == 1)
                    contactTypes.Add("Phone");
                if (s.PhoneAfterHrs == 1)
                    contactTypes.Add("Phone After Hours");
                if (s.PrankCall == 1)
                    contactTypes.Add("Prank Call");
                if (s.Email == 1)
                    contactTypes.Add("Email");
                if (s.OffSite == 1)
                    contactTypes.Add("Offsite");
                if (s.OutgoingCallMailEmail == 1)
                    contactTypes.Add("Outgoing Communication");

                string allContacts = "";
                for(int i = 0; i < contactTypes.Count; i++)
                {
                    allContacts += contactTypes[i];
                    if (i != contactTypes.Count - 1)
                        allContacts += ", ";
                }

                ServiceInfoList.Items.Add(new
                {
                    Date = s.Date,
                    NewContact = s.NewContact == 1 ? "Yes" : "",
                    NewWalk = s.NewWalkIn == 1 ? "Yes" : "",
                    Rep = s.RepBySomeoneElse ==1 ? "Yes" : "No",
                    ContactTypes = allContacts
                });

                //gets all services and items requested that belong to the client
                ServicesRequestedList.ItemsSource = clientController.allServices(c);
                ItemsRequestedList.ItemsSource = clientController.allItems(c);                

            }
        }
        #endregion
    }
}