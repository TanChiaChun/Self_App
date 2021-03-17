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
        public string taskName_wrap => WrapString(taskName, 20);
        public bool isDone { get; private set; } = false;
        public string project { get; private set; } = "General";
        public string section { get; private set; } = "General";
        public DateTime dueDate { get; private set; } = DateTime.MinValue.Date;
        public string dueDateStr => !dueDate.Equals(DateTime.MinValue.Date) ? dueDate.ToString(MyCls.DATE_FORMAT_DB) : "";
        public string dueDateStr_dayMonth => !dueDate.Equals(DateTime.MinValue.Date) ? dueDate.ToString(MyCls.DATE_FORMAT_DAY_MONTH) : "";
        public DateTime doDate { get; private set; } = DateTime.MinValue.Date;
        public string doDateStr => !doDate.Equals(DateTime.MinValue.Date) ? doDate.ToString(MyCls.DATE_FORMAT_DB) : "";
        public string hasDoDate => !doDate.Equals(DateTime.MinValue.Date) ? "|Do" : "";
        public DateTime startDate { get; private set; } = DateTime.MinValue.Date;
        public string startDateStr => !startDate.Equals(DateTime.MinValue.Date) ? startDate.ToString(MyCls.DATE_FORMAT_DB) : "";
        public string hasStartDate => !startDate.Equals(DateTime.MinValue.Date) ? "|St" : "";
        private MyCls.Priority _priority = MyCls.Priority.Normal;
        public string priority_str => _priority.ToString();
        public string priority_intStr => (int)_priority + "-" + _priority.ToString();
        private MyCls.MyDay _myDay = MyCls.MyDay.None;
        public string myDay_str => _myDay.ToString();
        public string myDay_intStr => (int)_myDay + "-"  + _myDay.ToString();
        public HashSet<string> tags { get; } = new HashSet<string>();
        public List<Tuple<bool, string>> steps { get; } = new List<Tuple<bool, string>>();
        public string note { get; private set; } = "";
        public bool myDay_notDone { get; } = false;
        private bool hasSteps = false;
        public string hasSteps_Str => hasSteps ? "|Su" : "";
        private bool hasNote = false;
        public string hasNote_Str => hasNote ? "|N" : "";

        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask() { }
        public MyTask(int pId)
        {
            id = pId;
        }

        public MyTask(string pTaskName, DateTime pDate, MyCls.DateType dateType)
        {
            taskName = pTaskName;
            switch (dateType)
            {
                case MyCls.DateType.Due:
                    dueDate = pDate;
                    break;
                case MyCls.DateType.Do:
                    doDate = pDate;
                    break;
            }
        }

        public MyTask(string pTaskName, DateTime pDueDate, DateTime pStartDate)
        {
            taskName = pTaskName;
            dueDate = pDueDate;
            startDate = pStartDate;
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

        public MyTask(int pId, string pTaskName, string pProj, string pSect, DateTime pDueDate, DateTime pDoDate, MyCls.Priority pPriority, MyCls.MyDay pMyDay, bool pMyDay_notDone)
        {
            id = pId;
            taskName = pTaskName;
            project = pProj;
            section = pSect;
            dueDate = pDueDate;
            doDate = pDoDate;
            _priority = pPriority;
            _myDay = pMyDay;
            myDay_notDone = pMyDay_notDone;
        }

        public MyTask(int pId, string pTaskName, bool pIsDone, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, bool pMyDay_notDone, bool pHasSteps, bool pHasNote)
        {
            id = pId;
            taskName = pTaskName;
            isDone = pIsDone;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;
            myDay_notDone = pMyDay_notDone;
            hasSteps = pHasSteps;
            hasNote = pHasNote;
        }

        public MyTask(int pId, string pTaskName, bool pIsDone, string pProj, string pSect, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, MyCls.Priority pPriority, MyCls.MyDay pMyDay, bool pMyDay_notDone)
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
            myDay_notDone = pMyDay_notDone;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public override string ToString()
        {
            return $"{id}-{taskName}";
        }

        private string WrapString(string input, int limit)
        {
            int i = 0;
            string output = "";
            foreach (char c in input)
            {
                if (i % limit == 0 && i != 0)
                {
                    output += "-\n";
                }
                output += c;
                i++;
            }
            return output;
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
