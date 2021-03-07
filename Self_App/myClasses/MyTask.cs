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

        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask(string pProj, string pSect, string pTaskName, bool pIsCompleted)
        {
            taskName = pTaskName;
            isCompleted = pIsCompleted;

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
            return $"{project}-{section}-{taskName}-{isCompleted.ToString()}";
        }
    }
}
