using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace se2_loon_hh
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                // If database can't be connected to, this will throw an exception
                Entities db = new Entities();
                var test = db.Clients.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database file cannot be found or is invalid! Application must terminate!", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
