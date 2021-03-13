using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_App.myClasses
{
    public class MyTask
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        private const string DATE_FORMAT = "yyyy-MM-dd";
        public int id { get; } = -1;
        public string taskName { get; private set; } = "";
        public bool isDone { get; private set; } = false;
        public string project { get; private set; } = "General";
        public string section { get; private set; } = "General";
        public DateTime dueDate { get; private set; } = DateTime.MinValue.Date;
        public DateTime doDate { get; private set; } = DateTime.MinValue.Date;
        public DateTime startDate { get; private set; } = DateTime.MinValue.Date;
        private Priority _priority = Priority.Normal;
        public string priority_str => _priority.ToString();
        public string priority_intStr => (int)_priority + "-" + _priority.ToString();
        private MyDay _myDay = MyDay.None;
        public string myDay_str => _myDay.ToString();
        public string myDay_intStr => (int)_myDay + "-"  + _myDay.ToString();
        public HashSet<string> tags { get; } = new HashSet<string>();
        public List<Tuple<bool, string>> steps { get; } = new List<Tuple<bool, string>>();
        public string note { get; private set; } = "";
        public string dueDateStr
        {
            get
            {
                string dateStr = !dueDate.Equals(DateTime.MinValue.Date) ? dueDate.ToString(DATE_FORMAT) : "";
                return dateStr;
            }
        }
        public string doDateStr
        {
            get
            {
                string dateStr = !doDate.Equals(DateTime.MinValue.Date) ? doDate.ToString(DATE_FORMAT) : "";
                return dateStr;
            }
        }
        public string startDateStr
        {
            get
            {
                string dateStr = !startDate.Equals(DateTime.MinValue.Date) ? startDate.ToString(DATE_FORMAT) : "";
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
        public MyTask(int pId)
        {
            id = pId;
        }

        public MyTask(string pId, string pTaskName, string pIsDone, string pProj, string pSect, string pDueDate, string pDoDate, string pStartDate, string pPriority, string pMyDay)
        {
            id = Int32.Parse(pId);
            taskName = pTaskName;
            isDone = Convert.ToBoolean(Int32.Parse(pIsDone));
            project = pProj;
            section = pSect;
            dueDate = DateTime.ParseExact(pDueDate, DATE_FORMAT, null);
            doDate = DateTime.ParseExact(pDoDate, DATE_FORMAT, null);
            startDate = DateTime.ParseExact(pStartDate, DATE_FORMAT, null);
            _priority = (Priority)Int32.Parse(pPriority);
            _myDay = (MyDay)Int32.Parse(pMyDay);
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public override string ToString()
        {
            return $"{taskName}-{isDone.ToString()}-{project}-{section}-{dueDate.ToString()}-{doDate.ToString()}-{startDate.ToString()}-{_priority.ToString()}-{_myDay.ToString()}-{tags.Count}-{steps.Count}-{note}";
        }

        public void UpdateTask_FromDb(string pTaskName, string pIsDone, string pProj, string pSect, string pDueDate, string pDoDate, string pStartDate, string pPriority, string pMyDay, string pTags, string pSteps, string pNote)
        {
            taskName = pTaskName;
            isDone = Convert.ToBoolean(Int32.Parse(pIsDone));
            project = pProj;
            section = pSect;
            dueDate = DateTime.ParseExact(pDueDate, DATE_FORMAT, null);
            doDate = DateTime.ParseExact(pDoDate, DATE_FORMAT, null);
            startDate = DateTime.ParseExact(pStartDate, DATE_FORMAT, null);
            _priority = (Priority)Int32.Parse(pPriority);
            _myDay = (MyDay)Int32.Parse(pMyDay);
            note = pNote;

            if (!String.IsNullOrEmpty(pTags))
            {
                string[] cTags = pTags.Split(';');
                foreach (string sub in cTags)
                {
                    if (String.IsNullOrEmpty(sub))
                    {
                        continue;
                    }
                    tags.Add(sub);
                }
            }
            
            if (!String.IsNullOrEmpty(pSteps))
            {
                string[] cSteps = pSteps.Split(';');
                foreach (string sub in cSteps)
                {
                    if (String.IsNullOrEmpty(sub))
                    {
                        continue;
                    }
                    string[] item = sub.Split('|');
                    steps.Add(new Tuple<bool, string>(Convert.ToBoolean(Int32.Parse(item[0])), item[1]));
                }
            }
        }
    }
}
