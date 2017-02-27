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
using System.Windows.Shapes;

namespace se2_loon_hh.Forms
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }
        
        private void HelpSelectionChanged(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            String selectionName = ((TreeViewItem)e.NewValue).Name;
            
            if(selectionName == "addClient")
            {
                textBlock.Text = "Add Client Information";                
            }
            if (selectionName == "viewClient")
            {
                textBlock.Text = "View Client Information";
            }

        }
    }
}
