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
        public int id { get; } = -1;
        public string taskName { get; private set; } = "";
        public bool isDone { get; private set; } = false;
        public string project { get; private set; } = "General";
        public string section { get; private set; } = "General";
        public DateTime dueDate { get; private set; } = DateTime.MinValue.Date;
        public DateTime doDate { get; private set; } = DateTime.MinValue.Date;
        public DateTime startDate { get; private set; } = DateTime.MinValue.Date;
        private MyCls.Priority _priority = MyCls.Priority.Normal;
        public string priority_str => _priority.ToString();
        public string priority_intStr => (int)_priority + "-" + _priority.ToString();
        private MyCls.MyDay _myDay = MyCls.MyDay.None;
        public string myDay_str => _myDay.ToString();
        public string myDay_intStr => (int)_myDay + "-"  + _myDay.ToString();
        public HashSet<string> tags { get; } = new HashSet<string>();
        public List<Tuple<bool, string>> steps { get; } = new List<Tuple<bool, string>>();
        public string note { get; private set; } = "";
        public string dueDateStr
        {
            get
            {
                string dateStr = !dueDate.Equals(DateTime.MinValue.Date) ? dueDate.ToString(MyCls.DATE_FORMAT_DB) : "";
                return dateStr;
            }
        }
        public string dueDateStr_dayMonth
        {
            get
            {
                string dateStr = !dueDate.Equals(DateTime.MinValue.Date) ? $"[Du]{dueDate.ToString(MyCls.DATE_FORMAT_DAY_MONTH)}" : "";
                return dateStr;
            }
        }
        public string doDateStr
        {
            get
            {
                string dateStr = !doDate.Equals(DateTime.MinValue.Date) ? doDate.ToString(MyCls.DATE_FORMAT_DB) : "";
                return dateStr;
            }
        }
        public string startDateStr
        {
            get
            {
                string dateStr = !startDate.Equals(DateTime.MinValue.Date) ? startDate.ToString(MyCls.DATE_FORMAT_DB) : "";
                return dateStr;
            }
        }
        
        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask(int pId)
        {
            id = pId;
        }

        public MyTask(int pId, string pTaskName, bool pIsDone, DateTime pDueDate)
        {
            id = pId;
            taskName = pTaskName;
            isDone = pIsDone;
            dueDate = pDueDate;
        }

        public MyTask(int pId, string pTaskName, string pProj, string pSect, DateTime pStartDate, MyCls.Priority pPriority)
        {
            id = pId;
            taskName = pTaskName;
            project = pProj;
            section = pSect;
            startDate = pStartDate;
            _priority = pPriority;
        }

        public MyTask(int pId, string pTaskName, string pProj, string pSect, DateTime pDueDate, DateTime pDoDate, MyCls.Priority pPriority, MyCls.MyDay pMyDay)
        {
            id = pId;
            taskName = pTaskName;
            project = pProj;
            section = pSect;
            dueDate = pDueDate;
            doDate = pDoDate;
            _priority = pPriority;
            _myDay = pMyDay;
        }

        public MyTask(int pId, string pTaskName, bool pIsDone, string pProj, string pSect, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, MyCls.Priority pPriority, MyCls.MyDay pMyDay)
        {
            id = pId;
            taskName = pTaskName;
            isDone = pIsDone;
            project = pProj;
            section = pSect;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;
            _priority = pPriority;
            _myDay = pMyDay;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public override string ToString()
        {
            return $"{id}-{taskName}-{isDone.ToString()}-{project}-{section}-{dueDate.ToString()}-{doDate.ToString()}-{startDate.ToString()}-{_priority.ToString()}-{_myDay.ToString()}-{tags.Count}-{steps.Count}-{note}";
        }

        public void UpdateTask_FromDb(string pTaskName, bool pIsDone, string pProj, string pSect, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, MyCls.Priority pPriority, MyCls.MyDay pMyDay, string pTags, string pSteps, string pNote)
        {
            taskName = pTaskName;
            taskName = pTaskName;
            isDone = pIsDone;
            project = pProj;
            section = pSect;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;
            _priority = pPriority;
            _myDay = pMyDay;
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
