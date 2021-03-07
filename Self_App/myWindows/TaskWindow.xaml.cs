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
using Self_App.myClasses;

namespace Self_App.myWindows
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        ///// Generic
        private MyFunctions f = new MyFunctions();
        
        public TaskWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Create";
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private bool IsInputsValid()
        {
            if (String.IsNullOrEmpty(cmBx_proj.Text))
            {
                MessageBox.Show("Project cannot be empty!");
                return false;
            }

            string badInput_proj = f.ValidateSqlInput(cmBx_proj.Text);
            if (badInput_proj != "")
            {
                MessageBox.Show($"{badInput_proj} is not allowed in Project!");
                return false;
            }

            return true;
        }

        private void CreateTask()
        {
            if (!IsInputsValid())
            {
                return;
            }

            MyTask cTask = new MyTask(cmBx_proj.Text);
            MessageBox.Show(cTask.ToString());
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                CreateTask();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
