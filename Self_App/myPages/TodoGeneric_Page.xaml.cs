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
        private MyCls.TodoGeneric todo;
        private List<MyTask> tasks = new List<MyTask>();
        private DataGrid dataGrid;
        public StackPanel stkPnl_proj { get; }

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoGeneric_Page(MyCls.TodoGeneric pTodo, StackPanel stkPnl)
        {
            // Generic
            InitializeComponent();

            // Specific
            stkPnl_proj = stkPnl;
            todo = pTodo;
            switch (todo)
            {
                case MyCls.TodoGeneric.Priority:
                    dataGrid = dataGrid_priority;
                    break;
                case MyCls.TodoGeneric.Blank:
                    dataGrid = dataGrid_blank;
                    break;
                case MyCls.TodoGeneric.All:
                    dataGrid = dataGrid_all;
                    break;
            }
            txtBlk_generic.Text = todo.ToString();
            dataGrid.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            switch (todo)
            {
                case MyCls.TodoGeneric.Priority:
                    tasks = Db.Select_TodoPriority();
                    break;
                case MyCls.TodoGeneric.Blank:
                    tasks = Db.Select_TodoBlank();
                    break;
                case MyCls.TodoGeneric.All:
                    tasks = Db.Select_TodoAll();
                    break;
            }
            dataGrid.ItemsSource = tasks;

            MyCls.RefreshProjectButtons(stkPnl_proj);
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
