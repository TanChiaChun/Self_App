using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Self_App.myWindows;

namespace Self_App.myClasses
{
    public static class MyCls
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        public static string DATE_FORMAT_DAY_MONTH = "d/M";
        public static string DATE_FORMAT_DB = "yyyy-MM-dd";
        public static string DATETIME_FORMAT_DB = "yyyy-MM-ddTHH:mm:ss";
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
            Do
        }

        public enum DateRange
        {
            Earlier,
            Today,
            Upcoming
        }

        public enum TodoGeneric
        {
            All
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public static string ValidateSqlInput(string input)
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

        public static Tuple<string, string> GenerateSql_DateRange(DateRange dateRng)
        {
            switch (dateRng)
            {
                case DateRange.Earlier:
                    return new Tuple<string, string>("<", "DESC");
                case DateRange.Today:
                    return new Tuple<string, string>("=", "ASC");
                case DateRange.Upcoming:
                    return new Tuple<string, string>(">", "ASC");
            }

            return new Tuple<string, string>("", "");
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
