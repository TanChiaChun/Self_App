using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Self_App.myClasses
{
    class MyCls
    {
        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public string ValidateSqlInput(string input)
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

        public bool IsTextInputValid(bool allowEmpty, string input, string type)
        {
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
    }
}
