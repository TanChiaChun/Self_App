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
        private TodoMyDay_Page todoMyDayPg = new TodoMyDay_Page();
        private TodoDue_Page todoDuePg = new TodoDue_Page();
        private TodoAll_Page todoAllPg = new TodoAll_Page();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoPage()
        {
            // Generic
            InitializeComponent();
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_task_create_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWin = new TaskWindow();
            taskWin.ShowDialog();

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

        private void btn_all_Click(object sender, RoutedEventArgs e)
        {
            todoAllPg.RefreshData();
            fr_todo.Content = todoAllPg;
        }
    }
}
