using se2_loon_hh.Forms.FormsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace se2_loon_hh.Controllers
{
    class ServiceController : IDisposable
    {
        Entities _db;//DB Context
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ServiceController()
        {
            _db = new Entities();//create DB Context
        }
        /// <summary>
        /// This function builds a list of ComboBoxPairs to be passed to the AddService Page.
        /// The ID and Full Name of the client are added to the pair and sent off to the page for display.
        /// </summary>
        /// <returns>List of clients using the ComboBoxPairs object</returns>
        public List<ComboBoxPairs> GetClientsForComboBox()
        {
            List<ComboBoxPairs> clientList = new List<ComboBoxPairs>();
            //loop through each client and add them to the combobox pairs list
            foreach (var client in _db.Clients.ToList())
            {
                clientList.Add(new ComboBoxPairs(client.Id.ToString(), client.FirstName + " " + client.MiddleName + " " + client.LastName));
            }
            return clientList;
        }
        public List<ComboBoxPairs> GetDonationTypes()
        {
            List<ComboBoxPairs> donationTypes = new List<ComboBoxPairs>();
            donationTypes.Add(new ComboBoxPairs("Boolean", "Yes/No"));
            donationTypes.Add(new ComboBoxPairs("Integer", "Number"));

            return donationTypes;
        }
        /// <summary>
        /// This function creates a service from data collected in the form.
        /// </summary>
        public void CreateService(Service service, ServiceRequested serviceRequested)
        {
            _db.Services.Add(service);
            _db.ServiceRequesteds.Add(serviceRequested);
            _db.SaveChanges();
        }
        /// <summary>
        /// This function disposes of the DB Context.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
