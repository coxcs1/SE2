using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace se2_loon_hh.Controllers
{
    class VolunteerController
    {
        Entities _db;//DB Context
        /// <summary>
        /// Default Constructor
        /// </summary>
        public VolunteerController()
        {
            _db = new Entities();//create DB Context
        }

        public List<object> GetAllVolunteers()
        {
            var volunteerList = new List<object>();//create a list of generic objects
            var volunteers = _db.Volunteers.ToList();//fetch all of the services requested
            //loop through each and add the information we need for the data grid
            foreach (var item in volunteers)
            {
                volunteerList.Add(new
                {
                    ID = item.Id,//used for navigating to a more detailed service view page
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    DateReceived = item.DateReceived,
                    Hours = item.Hours,
                    Service = item.Service,
                    Comment = item.Comment
                });
            }
            //send the list to the partial form class
            return volunteerList;
        }

        public Volunteer GetVolunteer(int volunteerID)
        {
            return _db.Volunteers.Find(volunteerID);
        }

        /// <summary>
        /// This function creates a volunteer from data collected in the form.
        /// </summary>
        public void CreateVolunteer(Volunteer volunteer)
        {
            _db.Volunteers.Add(volunteer);
            _db.SaveChanges();
        }

        public void EditVolunteer(Volunteer volunteer)
        {
            _db.Entry(volunteer).State = EntityState.Modified;
            _db.Entry(volunteer).State = EntityState.Modified;//let ORM know object has been modified
            _db.SaveChanges();//save the new changes to the DB
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}