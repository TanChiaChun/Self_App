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
        public string project { get; } = "General";
        public string section { get; } = "General";
        public string taskName { get; } = "";
        public bool isCompleted { get; } = false;
        public DateTime dueDate { get; } = DateTime.MinValue.Date;
        public DateTime doDate { get; } = DateTime.MinValue.Date;
        public DateTime startDate { get; } = DateTime.MinValue.Date;
        private Priority _priority = Priority.Normal;
        private MyDay _myDay = MyDay.None;
        public List<Tuple<bool, string>> steps { get; set; } = new List<Tuple<bool, string>>();
        public string note { get; } = "";
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
        public MyTask(string pProj, string pSect, string pTaskName, bool pIsCompleted, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate, int pPriority, int pMyDay, List<Tuple<bool, string>> pSteps, string pNote)
        {
            taskName = pTaskName;
            isCompleted = pIsCompleted;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;
            _priority = (Priority)pPriority;
            _myDay = (MyDay)pMyDay;
            steps = pSteps;
            note = pNote;

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
            return $"{project}-{section}-{taskName}-{isCompleted.ToString()}-{dueDate.ToString()}-{doDate.ToString()}-{startDate.ToString()}-{_priority.ToString()}-{_myDay.ToString()}-{steps.Count}-{note}";
        }
    }
}
