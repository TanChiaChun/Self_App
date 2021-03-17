using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Self_App.myWindows;

namespace Self_App.myClasses
{
    public static class MyCls
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        public static string DATE_FORMAT_DAY_MONTH = "d/M";
        public static string DATE_FORMAT_TIME_DATE = "h:mmtt (d/M)";
        public static string DATE_FORMAT_DB = "yyyy-MM-dd";
        public static string DATETIME_FORMAT_DB = "yyyy-MM-ddTHH:mm:ss";
        public static string DATETIME_FORMAT_VBA = "M/d/yyyy 12:00:00 A\\M";
        public static string SQL_COMMA = ", ";

        //////////////////////////////////////////////////
        // Enums
        //////////////////////////////////////////////////
        public enum ColorMode
        {
            Light,
            Dark
        }

        public enum Priority
        {
            Urgent,
            Important,
            Normal,
            Low
        }

        public enum MyDay
        {
            Urgent_Important,
            NotUrgent_Important,
            Urgent_NotImportant,
            NotUrgent_NotImportant,
            None
        }

        public enum DateType
        {
            Due,
            Do,
            Start
        }

        public enum DateRange
        {
            Earlier,
            Today,
            Upcoming
        }

        public enum TodoGeneric
        {
            Priority,
            Blank,
            All
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public static void ProcessDateTextBlock(TextBlock txtBlk, DateTime pDateTime, DateTime info, DateTime warning, string dateTimeFormat, string prefix)
        {
            txtBlk.Text = prefix + pDateTime.ToString(dateTimeFormat);
            if (pDateTime < warning)
            {
                txtBlk.Foreground = Brushes.Red;
            }
            else if (pDateTime < info)
            {
                txtBlk.Foreground = Brushes.Gold;
            }
        }

        public static void UpdateDateTextBlock(TextBlock txtBlk, string input)
        {
            txtBlk.Text = input;
            txtBlk.ClearValue(TextBlock.ForegroundProperty);
        }
        
        public static void RefreshProjectButtons(StackPanel stkPnl)
        {
            stkPnl.Children.Clear();
            List<string> projects = Db.Select_Projects();
            foreach (string project in projects)
            {
                Button btn = new Button();
                btn.Content = project;
                stkPnl.Children.Add(btn);
            }
        }

        private static string ValidateSqlInput(string input)
        {
            List<char> badChars = new List<char>() { '"', '\'', ';', '|' };

            foreach (char c in input)
            {
                foreach (char d in badChars)
                {
                    if (c.Equals(d))
                    {
                        return c.ToString();
                    }
                }
            }
            return "";
        }

        public static bool IsTextInputValid(string input, string type, int limit, bool allowEmpty)
        {
            if (input.Length > limit)
            {
                MessageBox.Show($"Exceed {limit} limit for {type}!");
                return false;
            }
            
            if (!allowEmpty && String.IsNullOrEmpty(input))
            {
                MessageBox.Show($"{type} cannot be empty!");
                return false;
            }

            string badInput_proj = ValidateSqlInput(input);
            if (badInput_proj != "")
            {
                MessageBox.Show($"{badInput_proj} is not allowed in {type}!");
                return false;
            }

            return true;
        }

        public static int RunPython(string pArgs)
        {
            int exitCode = -1;
            ProcessStartInfo myStart = new ProcessStartInfo();
            myStart.FileName = ConfigurationManager.AppSettings.Get("Python");
            myStart.Arguments = pArgs;
            myStart.UseShellExecute = false;
            myStart.CreateNoWindow = true;
            myStart.RedirectStandardOutput = true;
            myStart.RedirectStandardError = true;
            using (Process myProcess = Process.Start(myStart))
            {
                using (StreamReader myReader = myProcess.StandardOutput)
                {
                    string myStderr = myProcess.StandardError.ReadToEnd();
                    string myResult = myReader.ReadToEnd();
                    exitCode = myProcess.ExitCode;
                }
            }
            return exitCode;
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        public static bool DataGrid_Todo_MouseDoubleClick(ref DataGrid pDataGrid)
        {
            int i = pDataGrid.SelectedIndex;
            if (i == -1)
            {
                return false;
            }

            MyTask cTask = (MyTask)pDataGrid.Items[i];
            TaskWindow taskWin = new TaskWindow(Db.Select_Task(cTask.id));
            taskWin.ShowDialog();
            if (taskWin.toUpdate || taskWin.toDelete)
            {
                return true;
            }

            return false;
        }
    }
}
