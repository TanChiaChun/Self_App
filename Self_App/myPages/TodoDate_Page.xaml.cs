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
    /// Interaction logic for TodoDue_Page.xaml
    /// </summary>
    public partial class TodoDate_Page : Page, ITodo
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private List<MyTask> tasks_earlier = new List<MyTask>();
        private List<MyTask> tasks_today = new List<MyTask>();
        private List<MyTask> tasks_upcoming = new List<MyTask>();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoDate_Page()
        {
            // Generic
            InitializeComponent();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            tasks_earlier = Db.Select_TodoDue(MyCls.GenerateSql_DateRange(MyCls.DateRange.Earlier));
            dataGrid_earlier.ItemsSource = tasks_earlier;

            tasks_today = Db.Select_TodoDue(MyCls.GenerateSql_DateRange(MyCls.DateRange.Today));
            dataGrid_today.ItemsSource = tasks_today;

            tasks_upcoming = Db.Select_TodoDue(MyCls.GenerateSql_DateRange(MyCls.DateRange.Upcoming));
            dataGrid_upcoming.ItemsSource = tasks_upcoming;
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid cDataGrid = e.Source as DataGrid;
            if (MyCls.DataGrid_Todo_MouseDoubleClick(ref cDataGrid))
            {
                RefreshData();
            }
        }
    }
}
