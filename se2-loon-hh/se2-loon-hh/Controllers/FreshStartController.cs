using se2_loon_hh.Forms.FormsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace se2_loon_hh.Controllers
{
    class FreshStartController : IDisposable
    {
        Entities _db;

        public FreshStartController()
        {
            _db = new Entities();
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
        /// This function returns a list of fresh start programs for display in a data grid.
        /// </summary>
        /// <returns>List of generic objects for display in a data grid.</returns>
        public List<object> getAllFreshStartPrograms()
        {
            List<object> freshStartPrograms = new List<object>();//create list
            //loop through each fresh start program and add data to the list
            foreach (var freshStart in _db.FreshStarts.ToList())
            {
                freshStartPrograms.Add(new {
                    Name = freshStart.Name,
                    CurriculumType = freshStart.CurriculumType,
                    Comment = freshStart.Comment
                });
            }
            //send the list to the view
            return freshStartPrograms;
        }
        /// <summary>
        /// This function builds a list of fresh start programs that
        /// are ordered by curriculum type.
        /// </summary>
        /// <returns>List of object data for the data grid.</returns>
        public List<object> GetFreshStartsByType()
        {
            List<object> freshStarts = new List<object>();
            foreach (var freshStart in _db.FreshStarts.OrderBy(fs => fs.CurriculumType))
            {
                freshStarts.Add(new {
                    FreshStartID = freshStart.Id,
                    Name = freshStart.Name,
                    CurriculumType = freshStart.CurriculumType,
                    Comment = freshStart.Comment
                });
            }
            return freshStarts;
        }

        /// <summary>
        /// This function creates a list of fresh start types.
        /// </summary>
        /// <returns>List of fresh start types.</returns>
        public List<ComboBoxPairs> GetFreshStartTypes()
        {
            List<ComboBoxPairs> freshStartTypes = new List<ComboBoxPairs>();
            freshStartTypes.Add(new ComboBoxPairs("Parental", "Parental"));
            freshStartTypes.Add(new ComboBoxPairs("Motherhood", "Motherhood"));
            freshStartTypes.Add(new ComboBoxPairs("Hope House Class Electives", "Hope House Class Electives"));
            return freshStartTypes;
        }

        /// <summary>
        /// This function stores a fresh start program into the database.
        /// </summary>
        /// <param name="freshStart"></param>
        public void saveFreshStart(FreshStart freshStart)
        {
            _db.FreshStarts.Add(freshStart);
            _db.SaveChanges();
        }
        /// <summary>
        /// This function edits an exisiting fresh start.
        /// </summary>
        /// <param name="freshStart"></param>
        public void editFreshStart(FreshStart freshStart)
        {
            _db.Entry(freshStart).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
        /// <summary>
        /// This function fetches a fresh start from the database by id.
        /// </summary>
        /// <param name="freshStartID"></param>
        /// <returns></returns>
        public FreshStart GetFreshStart(int freshStartID)
        {
            return _db.FreshStarts.Find(freshStartID);
        }
        /// <summary>
        /// This function fetches a list of combobox pairs for the fresh start forms.
        /// </summary>
        /// <returns></returns>
        public List<ComboBoxPairs> GetAllFreshStarts()
        {
            List<ComboBoxPairs> freshStarts = new List<ComboBoxPairs>();

            foreach (var freshStart in _db.FreshStarts.OrderBy(fs => fs.CurriculumType).ToList())
            {
                freshStarts.Add(new ComboBoxPairs(freshStart.Id.ToString(), freshStart.Name));
            }

            return freshStarts;
        }

        public RelativeFreshStart GetRelative(int relativeID)
        {
            return _db.RelativeFreshStarts.Find(relativeID);
        }

        /// <summary>
        /// This function persists a relative into the database...
        /// </summary>
        /// <param name="relative"></param>
        public void saveRelative(RelativeFreshStart relative)
        {
            _db.RelativeFreshStarts.Add(relative);
            _db.SaveChanges();
        }
        /// <summary>
        /// This function updates a relative entity in the database...
        /// </summary>
        /// <param name="relative"></param>
        public void editRelative(RelativeFreshStart relative)
        {
            _db.Entry(relative).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
        /// <summary>
        /// This function gets a generic list of objects for the relatives data grid page...
        /// </summary>
        /// <returns></returns>
        public List<object> GetAllRelatives()
        {
            List<object> relatives = new List<object>();

            foreach (var relative in _db.RelativeFreshStarts.ToList())
            {
                relatives.Add(new {
                    RelativeID = relative.Id,
                    Relative = relative.FirstName + " " + relative.LastName,
                    Client = relative.Client.FirstName + " " + relative.Client.MiddleName + " " + relative.Client.LastName,
                    Relationship = relative.RelationshipTo,
                    FreshStart = relative.FreshStart.Name,
                    Date = relative.Date
                });
            }

            return relatives;
        }
        /// <summary>
        /// This function gets a list of years since 1970 up to this year..
        /// </summary>
        /// <returns>List of years since 1970</returns>
        public List<ComboBoxPairs> GetYears()
        {
            List<ComboBoxPairs> years = new List<ComboBoxPairs>();

            for (int i = (DateTime.Now.Year); i > 1970 ; i--)
            {
                years.Add(new ComboBoxPairs(Convert.ToString(i), Convert.ToString(i)));
            }

            return years;
        }
        /// <summary>
        /// This function returns all the months in a given year..
        /// </summary>
        /// <returns>List of months</returns>
        public List<ComboBoxPairs> GetMonths()
        {
            List<ComboBoxPairs> months = new List<ComboBoxPairs>() {
                new ComboBoxPairs("January", "January"),
                new ComboBoxPairs("February", "February"),
                new ComboBoxPairs("March", "March"),
                new ComboBoxPairs("April", "April"),
                new ComboBoxPairs("May", "May"),
                new ComboBoxPairs("June", "June"),
                new ComboBoxPairs("July", "July"),
                new ComboBoxPairs("August", "August"),
                new ComboBoxPairs("September", "September"),
                new ComboBoxPairs("October", "October"),
                new ComboBoxPairs("November", "November"),
                new ComboBoxPairs("December", "December"),
            };

            return months;
        }

        /// <summary>
        /// This function fetches a client fresh start from the database..
        /// </summary>
        /// <param name="clientFreshStartID"></param>
        /// <returns>A client fresh start entity...</returns>
        public ClientFreshStart GetClientFreshStart(int clientFreshStartID)
        {
            return _db.ClientFreshStarts.Find(clientFreshStartID);
        }

        /// <summary>
        /// This function saves a client into the database..
        /// </summary>
        /// <param name="client"></param>
        public void CreateClientFreshStart(ClientFreshStart client)
        {
            _db.ClientFreshStarts.Add(client);
            _db.SaveChanges();
        }

        /// <summary>
        /// This function save a modified client fresh start into the database..
        /// </summary>
        /// <param name="client"></param>
        public void EditClientFreshStart(ClientFreshStart client)
        {
            _db.Entry(client).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
        /// <summary>
        /// This function gets all client fresh start programs...
        /// </summary>
        /// <returns>A list of all client fresh start programs..</returns>
        public List<object> GetAllClientFreshStarts()
        {
            List<object> clientFreshStarts = new List<object>();

            foreach (var client in _db.ClientFreshStarts)
            {
                clientFreshStarts.Add(new {
                    ClientFreshStartID = client.Id,
                    Client = client.Client.FirstName + " " + client.Client.MiddleName + " " + client.Client.LastName,
                    FreshStart = client.FreshStart.Name,
                    Date = client.Date,
                    Status = client.Status
                });
            }

            return clientFreshStarts;
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
