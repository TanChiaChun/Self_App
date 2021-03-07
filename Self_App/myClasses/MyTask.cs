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

        //////////////////////////////////////////////////
        // Constructors
        //////////////////////////////////////////////////
        public MyTask(string pProj)
        {
            project = pProj;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public override string ToString()
        {
            return project;
        }
    }
}
