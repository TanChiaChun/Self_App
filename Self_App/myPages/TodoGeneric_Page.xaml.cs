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
using Self_App.myWindows;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoAll_Page.xaml
    /// </summary>
    public partial class TodoGeneric_Page : Page, ITodo
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private List<MyTask> tasks = new List<MyTask>();
        private DataGrid dataGrid;

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoGeneric_Page(MyCls.TodoGeneric todo)
        {
            // Generic
            InitializeComponent();

            // Specific
            switch (todo)
            {
                case MyCls.TodoGeneric.All:
                    txtBlk_generic.Text = "All";
                    dataGrid = dataGrid_all;
                    break;
            }
            dataGrid.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            tasks = Db.Select_TodoAll();
            dataGrid.ItemsSource = tasks;
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
