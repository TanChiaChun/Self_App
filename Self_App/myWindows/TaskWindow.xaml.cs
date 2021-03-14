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
        // Specific
        private MyTask task = new MyTask(-1);
        private List<string> projects = new List<string>();
        private List<string> sections = new List<string>();

        public TaskWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Create";
            InitWindow();
        }

        public TaskWindow(MyTask pTask)
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Update";
            btn_create.Visibility = Visibility.Collapsed;

            task = pTask;
            cmBx_proj.Text = task.project;
            cmBx_sect.Text = task.section;
            txtBx_task.Text = task.taskName;
            chkBx_task.IsChecked = task.isDone;
            datePick_due.SelectedDate = task.dueDate;
            datePick_do.SelectedDate = task.doDate;
            datePick_start.SelectedDate = task.startDate;
            cmBx_priority.Text = task.priority_str;
            cmBx_myDay.Text = task.myDay_str;
            txtBx_note.Text = task.note;

            sections = Db.Select_Sections(task.project);

            InitWindow();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void InitWindow()
        {
            projects = Db.Select_Projects();
            
            cmBx_proj.ItemsSource = projects;
            cmBx_sect.ItemsSource = sections;
            listBx_tag.ItemsSource = task.tags;
            dataGrid_steps.ItemsSource = task.steps;
        }

        private void RefreshSections(string project)
        {
            sections = Db.Select_Sections(project);
            cmBx_sect.ItemsSource = sections;
        }
        
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
            // Task Name
            if (!MyCls.IsTextInputValid(false, txtBx_task.Text, "Task Name"))
            {
                return false;
            }

            // Project
            if (!MyCls.IsTextInputValid(true, cmBx_proj.Text, "Project"))
            {
                return false;
            }

            // Section
            if (!MyCls.IsTextInputValid(true, cmBx_sect.Text, "Section"))
            {
                return false;
            }

            // Note
            if (!MyCls.IsTextInputValid(true, txtBx_note.Text, "Note"))
            {
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

            string pre = MyCls.SQL_COMMA;
            string dF = MyCls.DATE_FORMAT_DB;
            string dtF = MyCls.DATETIME_FORMAT_DB;

            // task_name
            string query_pre = "task_name";
            string query_post = $"'{txtBx_task.Text}'";

            // is_done
            query_pre += $"{pre}is_done";
            query_post += $"{pre}{Convert.ToInt32(chkBx_task.IsChecked)}";

            // project
            if (!String.IsNullOrEmpty(cmBx_proj.Text))
            {
                query_pre += $"{pre}project";
                query_post += $"{pre}'{cmBx_proj.Text}'";
            }

            // section
            if (!String.IsNullOrEmpty(cmBx_sect.Text))
            {
                query_pre += $"{pre}section";
                query_post += $"{pre}'{cmBx_sect.Text}'";
            }

            // due_date
            if (datePick_due.SelectedDate.HasValue)
            {
                query_pre += $"{pre}due_date";
                query_post += $"{pre}'{((DateTime)datePick_due.SelectedDate).ToString(dF)}'";
            }

            // do_date
            if (datePick_do.SelectedDate.HasValue)
            {
                query_pre += $"{pre}do_date";
                query_post += $"{pre}'{((DateTime)datePick_do.SelectedDate).ToString(dF)}'";
            }

            // start_date
            if (datePick_start.SelectedDate.HasValue)
            {
                query_pre += $"{pre}start_date";
                query_post += $"{pre}'{((DateTime)datePick_start.SelectedDate).ToString(dF)}'";
            }

            // priority
            query_pre += $"{pre}priority";
            query_post += $"{pre}{cmBx_priority.SelectedIndex}";

            // my_day
            query_pre += $"{pre}my_day";
            query_post += $"{pre}{cmBx_myDay.SelectedIndex}";

            // tags
            if (task.tags.Count > 0)
            {
                string tagQuery = "";
                foreach (string tag in task.tags)
                {
                    tagQuery += $"{tag};";
                }

                query_pre += $"{pre}tags";
                query_post += $"{pre}'{tagQuery}'";
            }

            // steps
            if (task.steps.Count > 0)
            {
                string stepQuery = "";
                foreach (Tuple<bool, string> step in task.steps)
                {
                    stepQuery += $"{Convert.ToInt32(step.Item1)}|{step.Item2};";
                }

                query_pre += $"{pre}steps";
                query_post += $"{pre}'{stepQuery}'";
            }

            // note
            query_pre += $"{pre}note";
            query_post += $"{pre}'{txtBx_note.Text}'";

            // create_date
            query_pre += $"{pre}create_date";
            query_post += $"{pre}'{DateTime.Now.ToString(dtF)}'";

            // modify_date
            query_pre += $"{pre}modify_date";
            query_post += $"{pre}'{DateTime.Now.ToString(dtF)}'";

            Db.WriteDb($"INSERT INTO Task({query_pre}) VALUES({query_post})");
            Close();
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

        private void cmBx_proj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmBx = e.Source as ComboBox;

            cmBx_sect.Text = "";
            RefreshSections(cmBx.Items[cmBx.SelectedIndex].ToString());
        }

        private void btn_tagAdd_Click(object sender, RoutedEventArgs e)
        {
            TagWindow tagWin = new TagWindow();
            tagWin.ShowDialog();
            if (tagWin.toAdd)
            {
                task.tags.Add(tagWin.tag);
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
                    task.tags.Remove(listBx.Items[i].ToString());
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
                task.steps.Add(stepWin.step);
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

            StepWindow stepWin = new StepWindow(task.steps[i]);
            stepWin.ShowDialog();

            // Update step
            if (stepWin.toUpdate)
            {
                task.steps[i] = stepWin.step;
                RefreshSteps();
            }
            // Delete step
            else if (stepWin.toDelete)
            {
                task.steps.RemoveAt(i);
                RefreshSteps();
            }
        }
    }
}
