using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Self_App.myWindows
{
    interface IWindow
    {
        void Window_KeyDown(object sender, KeyEventArgs e);
    }
}
