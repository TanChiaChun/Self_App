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

        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask(string pProj, string pSect, string pTaskName, bool pIsCompleted, DateTime pDueDate, DateTime pDoDate, DateTime pStartDate)
        {
            taskName = pTaskName;
            isCompleted = pIsCompleted;
            dueDate = pDueDate;
            doDate = pDoDate;
            startDate = pStartDate;

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
            return $"{project}-{section}-{taskName}-{isCompleted.ToString()}-{dueDate.ToString()}-{doDate.ToString()}-{startDate.ToString()}";
        }
    }
}
