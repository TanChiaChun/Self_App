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
        // Generic
        private MyFunctions f = new MyFunctions();

        // Specific
        private List<Tuple<bool, string>> steps = new List<Tuple<bool, string>>();
        private List<string> tags = new List<string>();

        public TaskWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Create";
            listBx_tag.ItemsSource = tags;
            dataGrid_steps.ItemsSource = steps;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void RefreshTags()
        {
            listBx_tag.Items.Refresh();
        }
        
        private void RefreshSteps()
        {
            dataGrid_steps.Items.Refresh();
        }

        private bool IsInputsValid()
        {
            // Project
            if (!f.IsTextInputValid(true, cmBx_proj.Text, "Project"))
            {
                return false;
            }

            // Section
            if (!f.IsTextInputValid(true, cmBx_sect.Text, "Section"))
            {
                return false;
            }

            // Project
            if (!f.IsTextInputValid(false, txtBx_task.Text, "Task Name"))
            {
                return false;
            }

            // Note
            if (!f.IsTextInputValid(true, txtBx_note.Text, "Note"))
            {
                return false;
            }

            return true;
        }

        private DateTime ValidateDate(DatePicker datePick)
        {
            if (datePick.SelectedDate.HasValue)
            {
                return (DateTime)datePick.SelectedDate;
            }
            return DateTime.MinValue.Date;
        }

        private void CreateTask()
        {
            if (!IsInputsValid())
            {
                return;
            }
            
            MyTask cTask = new MyTask(cmBx_proj.Text, cmBx_sect.Text, txtBx_task.Text, (bool)chkBx_task.IsChecked, ValidateDate(datePick_due), ValidateDate(datePick_do), ValidateDate(datePick_start), cmBx_priority.SelectedIndex, cmBx_myDay.SelectedIndex, steps, txtBx_note.Text);
            MessageBox.Show(cTask.ToString());
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        // Generic with differences
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

        // Specific
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }

        private void btn_tagAdd_Click(object sender, RoutedEventArgs e)
        {
            TagWindow tagWin = new TagWindow();
            tagWin.ShowDialog();
            if (tagWin.toAdd)
            {
                tags.Add(tagWin.tag);
                RefreshTags();
            }
        }

        private void listBx_tag_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBx = e.Source as ListBox;
            int i = listBx.SelectedIndex;
            if (i == -1)
            {
                return;
            }

            // Delete tag
            MessageBoxResult result = MessageBox.Show("Delete tag?", "Delete Tag", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    tags.RemoveAt(i);
                    RefreshTags();
                    break;
            }
        }

        private void btn_stepAdd_Click(object sender, RoutedEventArgs e)
        {
            StepWindow stepWin = new StepWindow();
            stepWin.ShowDialog();
            if (stepWin.toAdd)
            {
                steps.Add(stepWin.step);
                RefreshSteps();
            }
        }

        private void dataGrid_steps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid cDataGrid = e.Source as DataGrid;
            int i = cDataGrid.SelectedIndex;
            if (i == -1)
            {
                return;
            }

            StepWindow stepWin = new StepWindow(steps[i]);
            stepWin.ShowDialog();

            // Update step
            if (stepWin.toUpdate)
            {
                steps[i] = stepWin.step;
                RefreshSteps();
            }
            // Delete step
            else if (stepWin.toDelete)
            {
                steps.RemoveAt(i);
                RefreshSteps();
            }
        }
    }
}
