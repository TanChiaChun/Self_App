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
    /// Interaction logic for TodoMyDay_Page.xaml
    /// </summary>
    public partial class TodoMyDay_Page : Page
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private List<MyTask> tasks_0 = new List<MyTask>();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoMyDay_Page()
        {
            // Generic
            InitializeComponent();

            // Specific
            txtBlk_myDay.Text += DateTime.Now.ToString("d MMM");
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            tasks_0 = Db.Select_TodoMyDay(0);
            dataGrid_0.ItemsSource = tasks_0;
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void dataGrid_0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid cDataGrid = e.Source as DataGrid;
            if (MyCls.DataGrid_Todo_MouseDoubleClick(ref cDataGrid))
            {
                RefreshData();
            }
        }
    }
}
