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
    public partial class TodoMyDay_Page : Page, ITodo
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private List<MyTask> tasks_0 = new List<MyTask>();
        private List<MyTask> tasks_1 = new List<MyTask>();
        private List<MyTask> tasks_2 = new List<MyTask>();
        private List<MyTask> tasks_3 = new List<MyTask>();
        public StackPanel myStkPnl_proj { get; }

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoMyDay_Page(StackPanel stkPnl)
        {
            // Generic
            InitializeComponent();

            // Specific
            myStkPnl_proj = stkPnl;
            txtBlk_myDay.Text += DateTime.Now.ToString("d MMM");
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            tasks_0 = Db.Select_TodoMyDay(0);
            dataGrid_0.ItemsSource = tasks_0;

            tasks_1 = Db.Select_TodoMyDay(1);
            dataGrid_1.ItemsSource = tasks_1;

            tasks_2 = Db.Select_TodoMyDay(2);
            dataGrid_2.ItemsSource = tasks_2;

            tasks_3 = Db.Select_TodoMyDay(3);
            dataGrid_3.ItemsSource = tasks_3;

            MyCls.RefreshProjectButtons(myStkPnl_proj);
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid cDataGrid = e.Source as DataGrid;
            if (MyCls.DataGrid_Todo_MouseDoubleClick(ref cDataGrid))
            {
                RefreshData();
            }
        }
    }
}
