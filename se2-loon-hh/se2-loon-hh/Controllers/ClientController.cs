using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Controllers
{
    class ClientController : IDisposable
    {
        Entities _db;
        public ClientController()
        {
             _db = new Entities();
        }

        public void addClient()
        {
            //var volunteers = _db.Volunteers.ToList();
            //foreach(var volunteer in volunteers)
            //{
            //    Console.WriteLine(volunteer.DateHelped);
            //    Console.WriteLine(volunteer.Hours);
            //}
        }
        
        public List<Client> GetClientList()
        {
            return _db.Clients.ToList();
        }

        public Client GetSingleClient(int id)
        {
            return _db.Clients.Find(id);
        }

        public List<ProgressPointTracking> GetProgressPointsList(Client c)
        {
            return c.ProgressPointTrackings.ToList();
        }

        public string GetClientAge(Client c)
        {
            DateTime now = DateTime.Today;
            DateTime birth = Convert.ToDateTime(c.BirthDate);
            string yearsOld = ((int)(((now - birth).TotalDays) / 365)).ToString();
            return yearsOld;
        }

        #region Support Group Tab
        public int[] GetSupportGroupCountsForCurrentYear(Client c)
        {
            int[] sgCountCurrent = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            DateTime now = DateTime.Today;

            foreach(var clientFresh in c.ClientFreshStarts)
            {
                foreach(var support in clientFresh.SupportGroups)
                {
                    if(support.Year == now.Year)
                    {
                        IncrementMonthCount(ref sgCountCurrent, support.MonthAttended);
                    }
                }
            }           
            return sgCountCurrent;
        }

        public int[] GetSupportGroupCountsForPreviousYears(Client c)
        {
            int[] sgCountPrevious = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            DateTime now = DateTime.Today;

            foreach (var clientFresh in c.ClientFreshStarts)
            {
                foreach (var support in clientFresh.SupportGroups)
                {
                    if (support.Year < now.Year)
                    {
                        IncrementMonthCount(ref sgCountPrevious, support.MonthAttended);
                    }
                }
            }
            return sgCountPrevious;
        }

        public int[] GetMinMaxYearsForPreviousSG(Client c)
        {            
            DateTime now = DateTime.Today;
            int[] minMaxValues = new int[] { now.Year, 0 };

            foreach (var clientFresh in c.ClientFreshStarts)
            {
                foreach (var support in clientFresh.SupportGroups)
                {
                    if (support.Year > minMaxValues[1] && support.Year != now.Year)
                        minMaxValues[1] = (int)support.Year;
                    if (support.Year < minMaxValues[0])
                        minMaxValues[0] = (int)support.Year;
                }
            }
            return minMaxValues;
        }

        private void IncrementMonthCount(ref int[] count, string monthAttended)
        {
            if (monthAttended == "January" || monthAttended == "Jan." || monthAttended == "1")
                count[0]++;
            else if (monthAttended == "February" || monthAttended == "Feb." || monthAttended == "2")
                count[1]++;
            else if (monthAttended == "March" || monthAttended == "Mar." || monthAttended == "3")
                count[2]++;
            else if (monthAttended == "April" || monthAttended == "Apr." || monthAttended == "4")
                count[3]++;
            else if (monthAttended == "May" || monthAttended == "May" || monthAttended == "5")
                count[4]++;
            else if (monthAttended == "June" || monthAttended == "June" || monthAttended == "6")
                count[5]++;
            else if (monthAttended == "July" || monthAttended == "July" || monthAttended == "7")
                count[6]++;
            else if (monthAttended == "August" || monthAttended == "Aug." || monthAttended == "8")
                count[7]++;
            else if (monthAttended == "September" || monthAttended == "Sept." || monthAttended == "9")
                count[8]++;
            else if (monthAttended == "October" || monthAttended == "Oct." || monthAttended == "10")
                count[9]++;
            else if (monthAttended == "November" || monthAttended == "Nov." || monthAttended == "11")
                count[10]++;
            else //december
                count[11]++;
        }
        #endregion

        #region Course Attendance Tab
        public List<List<ClientFreshStart>> GetCoursesTaken(Client c)
        {
            List<ClientFreshStart> prenatalClasses = new List<ClientFreshStart>();
            List<ClientFreshStart> motherhoodClasses = new List<ClientFreshStart>();
            List<ClientFreshStart> electiveClasses = new List<ClientFreshStart>();

            //separates the classes based on the curriculum type
            foreach(ClientFreshStart clientFS in c.ClientFreshStarts)
            {
                if (clientFS.FreshStart.CurriculumType == "Prenatal")
                    prenatalClasses.Add(clientFS);
                else if (clientFS.FreshStart.CurriculumType == "Motherhood")
                    motherhoodClasses.Add(clientFS);
                else
                    electiveClasses.Add(clientFS);
            }

            List<List<ClientFreshStart>> allLists = new List<List<ClientFreshStart>>();
            allLists.Add(prenatalClasses);
            allLists.Add(motherhoodClasses);
            allLists.Add(electiveClasses);

            return allLists;
        }

        public List<RelativeFreshStart> GetRelativeCoursesTaken(Client c)
        {                       
            return c.RelativeFreshStarts.ToList();
        }
        #endregion

        #region Services Tab
        public List<ItemRequested> allItems(Client c)
        {
            List<ItemRequested> allItems = new List<ItemRequested>();

            foreach (ServiceInfo s in c.ServiceInfoes)
            {
                //concatenates onto the list
                allItems.AddRange(s.ItemRequesteds);
            }

            return allItems;
        }

        public List<ServiceRequested> allServices(Client c)
        {
            List<ServiceRequested> allServices = new List<ServiceRequested>();

            //concatenates onto the list
            foreach (ServiceInfo s in c.ServiceInfoes)
            {
                allServices.AddRange(s.ServiceRequesteds);
            }

            return allServices;
        }
        #endregion

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
