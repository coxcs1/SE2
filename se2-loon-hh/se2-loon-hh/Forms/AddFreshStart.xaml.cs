using se2_loon_hh.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for AddFreshStart.xaml
    /// </summary>
    public partial class AddFreshStart : Page
    {
        FreshStartController freshStartController;
        FreshStart freshStart;
        bool edit = false;
        public AddFreshStart()
        {
            freshStartController = new FreshStartController();
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.freshStart == null)
            {
                this.freshStart = new FreshStart();
            }
            else
            {
                edit = true;
            }

            populateFreshStart();

            if (!edit)
            {
                freshStartController.saveFreshStart(this.freshStart);
                this.NavigationService.Navigate(new MainPage());
            }
            
        }

        private void populateFreshStart()
        {
            this.freshStart.Name = FreshStartName.Text;
            this.freshStart.CurriculumType = FreshStartType.Text;
            this.freshStart.Comment = FreshStartComment.Text;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
