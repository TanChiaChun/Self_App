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
using Self_App.myWindows;
using Self_App.myPages;
using Self_App.myClasses;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoPage.xaml
    /// </summary>
    public partial class TodoPage : Page
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private TodoMyDay_Page todoMyDayPg;
        private TodoDate_Page todoDuePg;
        private TodoDate_Page todoDoPg;
        private TodoGeneric_Page todoPriorityPg;
        private TodoGeneric_Page todoBlankPg;
        private TodoGeneric_Page todoAllPg;
        private TodoProject_Page todoProjPg;

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoPage()
        {
            // Generic
            InitializeComponent();

            // Specific
            todoMyDayPg = new TodoMyDay_Page(stkPnl_proj);
            todoDuePg = new TodoDate_Page(MyCls.DateType.Due, stkPnl_proj);
            todoDoPg = new TodoDate_Page(MyCls.DateType.Do, stkPnl_proj);
            todoPriorityPg = new TodoGeneric_Page(MyCls.TodoGeneric.Priority, stkPnl_proj);
            todoBlankPg = new TodoGeneric_Page(MyCls.TodoGeneric.Blank, stkPnl_proj);
            todoAllPg = new TodoGeneric_Page(MyCls.TodoGeneric.All, stkPnl_proj);
            todoProjPg = new TodoProject_Page();
            RefreshData();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void RefreshData()
        {
            MyCls.RefreshProjectButtons(stkPnl_proj);
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_task_create_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWin = new TaskWindow();
            taskWin.ShowDialog();

            RefreshData();
            if (fr_todo.Content != null)
            {
                ((ITodo)fr_todo.Content).RefreshData();
            }
        }

        private void btn_myDay_Click(object sender, RoutedEventArgs e)
        {
            todoMyDayPg.RefreshData();
            fr_todo.Content = todoMyDayPg;
        }

        private void btn_due_Click(object sender, RoutedEventArgs e)
        {
            todoDuePg.RefreshData();
            fr_todo.Content = todoDuePg;
        }

        private void btn_do_Click(object sender, RoutedEventArgs e)
        {
            todoDoPg.RefreshData();
            fr_todo.Content = todoDoPg;
        }

        private void btn_priority_Click(object sender, RoutedEventArgs e)
        {
            todoPriorityPg.RefreshData();
            fr_todo.Content = todoPriorityPg;
        }

        private void btn_blank_Click(object sender, RoutedEventArgs e)
        {
            todoBlankPg.RefreshData();
            fr_todo.Content = todoBlankPg;
        }

        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
            todoAllPg.RefreshData();
            fr_todo.Content = todoAllPg;
        }

        private void btn_project_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            todoProjPg.UpdateProject((string)btn.Content);
            todoProjPg.RefreshData();
            fr_todo.Content = todoProjPg;
        }
    }
}
