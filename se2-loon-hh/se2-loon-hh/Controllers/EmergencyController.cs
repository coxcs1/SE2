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

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
