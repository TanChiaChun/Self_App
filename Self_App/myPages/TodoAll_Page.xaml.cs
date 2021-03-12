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
using Self_App.myClasses;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoAll_Page.xaml
    /// </summary>
    public partial class TodoAll_Page : Page
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Generic
        private Database db = new Database();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoAll_Page()
        {
            // Generic
            InitializeComponent();
            dataGrid_todoAll.ItemsSource = db.Select_TodoAll();
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
    }
}
