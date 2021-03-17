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
    public partial class TaskWindow : Window, IWindow
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private MyWrite type;
        private MyTask task = new MyTask();
        private List<string> projects = new List<string>();
        private List<string> sections = new List<string>();
        private bool hasDate_due = false;
        private bool hasDate_do = false;
        private bool hasDate_start = false;
        public HashSet<string> tags { get; } = new HashSet<string>();
        public List<Tuple<bool, string>> steps { get; } = new List<Tuple<bool, string>>();
        public bool toUpdate { get; private set; } = false;
        public bool toDelete { get; private set; } = false;
        private enum MyWrite
        {
            Create,
            Update
        }

        public TaskWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            type = MyWrite.Create;
            this.Title += " - Create";
            btn_update.Visibility = Visibility.Collapsed;
            btn_delete.Visibility = Visibility.Collapsed;

            InitWindow_Generic();
        }

        public TaskWindow(MyTask pTask)
        {
            // Generic
            InitializeComponent();

            // Specific
            type = MyWrite.Update;
            this.Title += " - Update";
            btn_create.Visibility = Visibility.Collapsed;

            task = pTask;
            cmBx_proj.Text = task.project;
            cmBx_sect.Text = task.section;
            txtBx_task.Text = task.taskName;
            chkBx_task.IsChecked = task.isDone;
            if (!task.dueDate.Equals(DateTime.MinValue))
            {
                datePick_due.SelectedDate = task.dueDate;
                hasDate_due = true;
            }
            if (!task.doDate.Equals(DateTime.MinValue))
            {
                datePick_do.SelectedDate = task.doDate;
                hasDate_do = true;
            }
            if (!task.startDate.Equals(DateTime.MinValue))
            {
                datePick_start.SelectedDate = task.startDate;
                hasDate_start = true;
            }
            cmBx_priority.Text = task.priority_str;
            cmBx_myDay.Text = task.myDay_str;
            txtBx_note.Text = task.note;

            sections = Db.Select_Sections(task.project);
            foreach (string tag in task.tags)
            {
                tags.Add(tag);
            }
            foreach (Tuple<bool, string> step in task.steps)
            {
                steps.Add(step);
            }

            InitWindow_Generic();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void InitWindow_Generic()
        {
            projects = Db.Select_Projects();
            
            cmBx_proj.ItemsSource = projects;
            cmBx_sect.ItemsSource = sections;
            listBx_tag.ItemsSource = tags;
            dataGrid_steps.ItemsSource = steps;
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

        private bool HasChange()
        {
            // project
            if (cmBx_proj.Text != task.project)
            {
                return true;
            }

            // section
            if (cmBx_sect.Text != task.section)
            {
                return true;
            }

            // task_name
            if (txtBx_task.Text != task.taskName)
            {
                return true;
            }

            // is_done
            if (chkBx_task.IsChecked != task.isDone)
            {
                return true;
            }

            // due_date
            if (datePick_due.SelectedDate.HasValue != hasDate_due)
            {
                return true;
            }
            else if (datePick_due.SelectedDate.HasValue && datePick_due.SelectedDate != task.dueDate)
            {
                return true;
            }

            // do_date
            if (datePick_do.SelectedDate.HasValue != hasDate_do)
            {
                return true;
            }
            else if (datePick_do.SelectedDate.HasValue && datePick_do.SelectedDate != task.doDate)
            {
                return true;
            }

            // start_date
            if (datePick_start.SelectedDate.HasValue != hasDate_start)
            {
                return true;
            }
            else if (datePick_start.SelectedDate.HasValue && datePick_start.SelectedDate != task.startDate)
            {
                return true;
            }

            // priority
            if (cmBx_priority.Text != task.priority_str)
            {
                return true;
            }

            // my_day
            if (cmBx_myDay.Text != task.myDay_str)
            {
                return true;
            }

            // tags
            if (!tags.SetEquals(task.tags))
            {
                return true;
            }

            // steps
            if (!steps.SequenceEqual(task.steps))
            {
                return true;
            }

            // note
            if (txtBx_note.Text != task.note)
            {
                return true;
            }

            return false;
        }

        private bool IsInputsValid(bool allowEmpty_projectNSection)
        {
            // task_name
            if (!MyCls.IsTextInputValid(txtBx_task.Text, "Task Name", 150, false))
            {
                return false;
            }

            // project
            if (!MyCls.IsTextInputValid(cmBx_proj.Text, "Project", 50, allowEmpty_projectNSection))
            {
                return false;
            }

            // section
            if (!MyCls.IsTextInputValid(cmBx_sect.Text, "Section", 50, allowEmpty_projectNSection))
            {
                return false;
            }

            // note
            if (!MyCls.IsTextInputValid(txtBx_note.Text, "Note", 1000, true))
            {
                return false;
            }

            // start_date
            if (datePick_start.SelectedDate.HasValue && !datePick_due.SelectedDate.HasValue)
            {
                MessageBox.Show("Due date cannot be empty!");
                return false;
            }
            else if (datePick_start.SelectedDate.HasValue && datePick_due.SelectedDate.HasValue && datePick_start.SelectedDate >= datePick_due.SelectedDate)
            {
                MessageBox.Show("Due date must be greater than start date!");
                return false;
            }

            return true;
        }

        private void CreateTask()
        {
            if (!IsInputsValid(true))
            {
                return;
            }

            string pre = MyCls.SQL_COMMA;
            string dF = MyCls.DATE_FORMAT_DB;
            string dtF = MyCls.DATETIME_FORMAT_DB;
            string currDatetime = DateTime.Now.ToString(dtF);

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
            if (tags.Count > 0)
            {
                string tagQuery = "";
                foreach (string tag in tags)
                {
                    tagQuery += $"{tag};";
                }

                query_pre += $"{pre}tags";
                query_post += $"{pre}'{tagQuery}'";
            }

            // steps
            if (steps.Count > 0)
            {
                string stepQuery = "";
                foreach (Tuple<bool, string> step in steps)
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
            query_post += $"{pre}'{currDatetime}'";

            // modify_date
            query_pre += $"{pre}modify_date";
            query_post += $"{pre}'{currDatetime}'";

            // complete_date
            if ((bool)chkBx_task.IsChecked)
            {
                query_pre += $"{pre}complete_date";
                query_post += $"{pre}'{currDatetime}'";
            }
            
            Db.WriteDb($"INSERT INTO Task({query_pre}) VALUES({query_post})");
            Close();
        }

        private void UpdateTask()
        {
            toUpdate = HasChange();
            if (!toUpdate)
            {
                MessageBox.Show("Nothing to update");
                return;
            }

            if (!IsInputsValid(false))
            {
                return;
            }

            string pre = MyCls.SQL_COMMA;
            string dF = MyCls.DATE_FORMAT_DB;
            string dtF = MyCls.DATETIME_FORMAT_DB;
            string currDatetime = DateTime.Now.ToString(dtF);

            // task_name
            string query = $"task_name='{txtBx_task.Text}'";

            // is_done
            query += $"{pre}is_done={Convert.ToInt32(chkBx_task.IsChecked)}";

            // project
            query += $"{pre}project='{cmBx_proj.Text}'";

            // section
            query += $"{pre}section='{cmBx_sect.Text}'";

            // due_date
            string dueDateStr = datePick_due.SelectedDate.HasValue ? ((DateTime)datePick_due.SelectedDate).ToString(dF) : "0001-01-01";
            query += $"{pre}due_date='{dueDateStr}'";

            // do_date
            string doDateStr = datePick_do.SelectedDate.HasValue ? ((DateTime)datePick_do.SelectedDate).ToString(dF) : "0001-01-01";
            query += $"{pre}do_date='{doDateStr}'";

            // start_date
            string startDateStr = datePick_start.SelectedDate.HasValue ? ((DateTime)datePick_start.SelectedDate).ToString(dF) : "0001-01-01";
            query += $"{pre}start_date='{startDateStr}'";

            // priority
            query += $"{pre}priority={cmBx_priority.SelectedIndex}";

            // my_day
            query += $"{pre}my_day={cmBx_myDay.SelectedIndex}";

            // tags
            string tagsStr = "";
            if (tags.Count > 0)
            {
                foreach (string tag in tags)
                {
                    tagsStr += $"{tag};";
                }
            }
            query += $"{pre}tags='{tagsStr}'";

            // steps
            string stepsStr = "";
            if (steps.Count > 0)
            {
                foreach (Tuple<bool, string> step in steps)
                {
                    stepsStr += $"{Convert.ToInt32(step.Item1)}|{step.Item2};";
                }
            }
            query += $"{pre}steps='{stepsStr}'";

            // note
            query += $"{pre}note='{txtBx_note.Text}'";

            // modify_date
            query += $"{pre}modify_date='{currDatetime}'";

            // complete_date
            if ((bool)chkBx_task.IsChecked)
            {
                query += $"{pre}complete_date='{currDatetime}'";
            }

            Db.WriteDb($"UPDATE Task SET {query} WHERE id={task.id}");
            Close();
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        // Generic with differences
        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (type == MyWrite.Create)
                {
                    CreateTask();
                }
                else if (type == MyWrite.Update)
                {
                    UpdateTask();
                }
            }
            else if (e.Key == Key.Escape)
            {
                toUpdate = HasChange();
                if (type == MyWrite.Update && toUpdate)
                {
                    MessageBoxResult result = MessageBox.Show("Unsave changes, continue?", "Unsave Changes", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    switch (result)
                    {
                        case MessageBoxResult.No:
                            return;
                    }
                    
                }

                Close();
            }
        }

        // Specific
        private void btn_create_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            UpdateTask();
        }

        private void cmBx_proj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmBx = e.Source as ComboBox;

            cmBx_sect.Text = "";
            if (cmBx.SelectedIndex != -1)
            {
                RefreshSections(cmBx.Items[cmBx.SelectedIndex].ToString());
            }
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
                    tags.Remove(listBx.Items[i].ToString());
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

            Tuple<bool, string> cStep = (Tuple<bool, string>)cDataGrid.Items[i];
            StepWindow stepWin = new StepWindow(cStep);
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

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Delete task?", "Delete Task", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    toDelete = true;
                    Db.Delete_Task(task.id);
                    Close();
                    break;
            }
        }
    }
}
