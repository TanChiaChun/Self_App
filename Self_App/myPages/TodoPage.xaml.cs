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

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoPage.xaml
    /// </summary>
    public partial class TodoPage : Page
    {
        public TodoPage()
        {
            InitializeComponent();
        }

        private void btn_task_create_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWin = new TaskWindow();
            taskWin.ShowDialog();
        }
    }
}
