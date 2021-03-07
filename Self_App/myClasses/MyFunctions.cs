using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_App.myClasses
{
    class MyFunctions
    {
        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public string ValidateSqlInput(string input)
        {
            List<char> badChars = new List<char>() { '"', '\'', ';' };

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
    }
}
