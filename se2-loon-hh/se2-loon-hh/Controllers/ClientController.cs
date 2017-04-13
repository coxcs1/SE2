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

        public void addClient(Client client)
        {
            _db.Clients.Add(client);
            _db.SaveChanges();
        }

        public void addClient(Client client, Pregnancy preg)
        {
            _db.Clients.Add(client);
            _db.Pregnancies.Add(preg);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
