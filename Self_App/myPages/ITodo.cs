using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Self_App.myPages
{
    interface ITodo
    {
        StackPanel myStkPnl_proj { get; }

        void RefreshData();
    }
}
