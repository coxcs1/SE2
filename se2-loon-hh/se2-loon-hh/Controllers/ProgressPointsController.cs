using se2_loon_hh.Forms.FormsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace se2_loon_hh.Controllers
{
    class ProgressPointsController
    {
        Entities _db;

        public ProgressPointsController()
        {
            _db = new Entities();
        }
        /// <summary>
        /// This function builds a list of ComboBoxPairs to be passed to the EntryProgressPointsPage Page.
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

        public List<Client> clientList()
        {
            return _db.Clients.ToList();
        }

        public void CreatePoints(ProgressPointTracking points)
        {
            _db.ProgressPointTrackings.Add(points);
            _db.SaveChanges();
        }

        public void EditPoints(ProgressPointTracking points)
        {
            _db.Entry(points).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Client GetClient(int clientID)
        {
            return _db.Clients.Find(clientID);
        }

        public ProgressPointTracking GetPoints(int pointsID)
        {
            return _db.ProgressPointTrackings.Find(pointsID);
        }


        /// <summary>
        /// This function returns a list to the data grid on the ViewProgressPointsPage
        /// </summary>
        /// <returns>pointsList</returns>
        public List<object> GetAllPoints()
        {
            var pointsList = new List<object>();//create a list of generic objects
            var points = _db.ProgressPointTrackings.ToList();//fetch all of the services requested

            //loop through each and add the information we need for the data grid
            foreach (var item in points)
            {
                pointsList.Add(new
                {
                    PointsID = item.Id,
                    Client = item.Client.FirstName + " " + item.Client.MiddleName + " " + item.Client.LastName,
                    CurrentPoints = item.Client.ProgressPoints,
                    Description = item.Description,
                    //ClientID = item.ClientId,
                    Date = item.Date,
                    // PointsSpent = item.PointsSpent
                });
            }
            //send the list to the partial form class
            return pointsList;
        }


        /// <summary>
        /// This function increase or decreases a clients current progress points
        /// if the operation param is true it subtracts
        /// else it adds
        /// </summary>
        /// <param name="pointsId"></param>
        /// <param name="clientId"></param>
        /// <param name="operation"></param>
        public void calculatePoints(int pointsId, int clientId, bool operation)
        {
            ProgressPointTracking points = _db.ProgressPointTrackings.Find(pointsId);
            Client client = _db.Clients.Find(clientId);

            if (operation)//if operation is true subtract
            {
                client.ProgressPoints -= points.PointsSpent;
            }
            else//if operation is false add  
            {
                client.ProgressPoints += points.PointsSpent;
            }

            _db.Entry(client).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}