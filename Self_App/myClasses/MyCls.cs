using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Self_App.myClasses
{
    static class MyCls
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        public static string DATE_FORMAT_DB = "yyyy-MM-dd";
        public static string DATETIME_FORMAT_DB = "yyyy-MM-ddThh:mm:ss";
        public static string SQL_COMMA = ", ";

        //////////////////////////////////////////////////
        // Enums
        //////////////////////////////////////////////////
        public enum ColorMode
        {
            Light,
            Dark
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public static string ValidateSqlInput(string input)
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

        public static bool IsTextInputValid(bool allowEmpty, string input, string type)
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
