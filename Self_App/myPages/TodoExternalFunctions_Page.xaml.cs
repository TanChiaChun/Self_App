using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Self_App.myClasses;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoExternal_Page.xaml
    /// </summary>
    public partial class TodoExternalFunctions_Page : Page, ITodo
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        public StackPanel myStkPnl_proj { get; }

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoExternalFunctions_Page()
        {
            // Generic
            InitializeComponent();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            txtBlk_write_calendar.Text = Db.Select_Hour("W_Calendar").ToString(MyCls.DATE_FORMAT_TIME_DATE);
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_write_calendar_Click(object sender, RoutedEventArgs e)
        {
            DateTime cDateTime = DateTime.Now;
            Db.Update_Hour(cDateTime, "W_Calendar");
            txtBlk_write_calendar.Text = cDateTime.ToString(MyCls.DATE_FORMAT_TIME_DATE);
        }
    }
}
