using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace se2_loon_hh.Controllers
{
    class EmergencyController : IDisposable
    {
        Entities _db;

        public EmergencyController()
        {
            _db = new Entities();
        }

        public void addEmergency(Emergency emergency)
        {
            _db.Emergencies.Add(emergency);
            _db.SaveChanges();            
        }

        public List<Emergency> viewAllEmergencies()
        {
            List<Emergency> emergencies = _db.Emergencies.ToList();
            return emergencies;
        }

        public Emergency viewEmergency(long id)
        {
            return _db.Emergencies.Find(id);
        }

        public void editEmergency(Emergency newEmergency)
        {
            Emergency oldEmergency = _db.Emergencies.Find(newEmergency.Id);
            oldEmergency = newEmergency;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
