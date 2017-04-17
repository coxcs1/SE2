using se2_loon_hh.Forms.FormsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
        
        /// <summary>
        /// This function creates a service from data collected in the form.
        /// </summary>
        public void CreateService(ServiceInfo service)
        {
            _db.ServiceInfoes.Add(service);//add service info to table
            _db.SaveChanges();//persist changes to the DB
        }
        /// <summary>
        /// This function persists an existing service.
        /// </summary>
        /// <param name="service"></param>
        public void EditService(ServiceInfo service)
        {
            _db.Entry(service).State = EntityState.Modified;//let ORM know object has been modified
            _db.SaveChanges();//save the new changes to the DB
        }
        /// <summary>
        /// Build a list of breif service information and package it
        /// up for the view services page.
        /// </summary>
        /// <returns>List of generic objects for the data grid</returns>
        public List<object> GetAllServices()
        {
            var serviceList = new List<object>();//create a list of generic objects
            var services = _db.ServiceInfoes.ToList();//fetch all of the services requested
            //loop through each and add the information we need for the data grid
            foreach (var service in services)
            {
                serviceList.Add(new {
                    ServiceID = service.Id,
                    Client = service.Client.FirstName + " " + service.Client.MiddleName + " " + service.Client.LastName,
                    Date = service.Date,
                    Items = service.ItemRequesteds.Count.ToString(),
                    ServicesRequested = service.ServiceRequesteds.Count.ToString()
                });
            }
            //send the list to the partial form class
            return serviceList;
        }
        public ServiceInfo GetServiceInfo(int serviceInfoID)
        {
            return _db.ServiceInfoes.Find(serviceInfoID);
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
