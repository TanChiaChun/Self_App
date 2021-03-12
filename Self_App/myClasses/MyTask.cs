using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_App.myClasses
{
    class MyTask
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        private const string DATE_FORMAT = "yyyy-MM-dd";
        public int id { get; } = -1;
        public string project { get; } = "General";
        public string section { get; } = "General";
        public string taskName { get; } = "";
        public bool isCompleted { get; } = false;
        public DateTime dueDate { get; } = DateTime.MinValue.Date;
        public DateTime doDate { get; } = DateTime.MinValue.Date;
        public DateTime startDate { get; } = DateTime.MinValue.Date;
        private MyDay _myDay = MyDay.None;
        public string myDay => (int)_myDay + "-"  + _myDay.ToString();
        private Priority _priority = Priority.Normal;
        public string priority => (int)_priority + "-" + _priority.ToString();
        public List<string> tags { get; } = new List<string>();
        public List<Tuple<bool, string>> steps { get; } = new List<Tuple<bool, string>>();
        public string note { get; } = "";
        public string dueDateStr
        {
            get
            {
                string dateStr = !dueDate.Equals(DateTime.MinValue.Date) ? dueDate.ToString("DATE_FORMAT") : "";
                return dateStr;
            }
        }
        public string doDateStr
        {
            get
            {
                string dateStr = !doDate.Equals(DateTime.MinValue.Date) ? doDate.ToString("DATE_FORMAT") : "";
                return dateStr;
            }
        }
        public string startDateStr
        {
            get
            {
                string dateStr = !startDate.Equals(DateTime.MinValue.Date) ? startDate.ToString("DATE_FORMAT") : "";
                return dateStr;
            }
        }
        private enum Priority
        {
            Urgent,
            Important,
            Normal,
            Low
        }
        private enum MyDay
        {
            Urgent_Important,
            NotUrgent_Important,
            Urgent_NotImportant,
            NotUrgent_NotImportant,
            None
        }

        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask(int pId, string pProj, string pSect, string pTaskName, bool pIsCompleted, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, int pMyDay, int pPriority)
        {
            id = pId;
            taskName = pTaskName;
            isCompleted = pIsCompleted;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;
            _myDay = (MyDay)pMyDay;
            _priority = (Priority)pPriority;

            if (pProj != "")
            {
                project = pProj;
            }
            if (pSect != "")
            {
                section = pSect;
            }
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public override string ToString()
        {
            return $"{project}-{section}-{taskName}-{isCompleted.ToString()}-{dueDate.ToString()}-{doDate.ToString()}-{startDate.ToString()}-{_myDay.ToString()}-{_priority.ToString()}-{tags.Count}-{steps.Count}-{note}";
        }
    }
}
