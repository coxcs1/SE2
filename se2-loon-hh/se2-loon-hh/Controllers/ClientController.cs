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
            var volunteers = _db.Volunteers.ToList();
            foreach(var volunteer in volunteers)
            {
                Console.WriteLine(volunteer.DateHelped);
                Console.WriteLine(volunteer.Hours);
            }
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
