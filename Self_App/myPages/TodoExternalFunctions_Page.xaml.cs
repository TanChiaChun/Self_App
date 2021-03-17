﻿using System;
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
        private TextBlock myTxtBlk_cal;
        public StackPanel myStkPnl_proj { get; }

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoExternalFunctions_Page(DateTime calLastChk, TextBlock txtBlk)
        {
            // Generic
            InitializeComponent();

            // Specific
            MyCls.ProcessDateTextBlock(txtBlk_write_calendar, calLastChk, DateTime.Today, DateTime.Today.AddDays(-1), MyCls.DATE_FORMAT_TIME_DATE, "");
            myTxtBlk_cal = txtBlk;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_write_calendar_Click(object sender, RoutedEventArgs e)
        {
            DateTime cDateTime = DateTime.Now;
            string cDateTime_str = cDateTime.ToString(MyCls.DATE_FORMAT_TIME_DATE);
            Db.Update_Hour(cDateTime, "W_Calendar");
            MyCls.UpdateDateTextBlock(txtBlk_write_calendar, cDateTime_str);
            MyCls.UpdateDateTextBlock(myTxtBlk_cal, $"Calendar last refresh: {cDateTime_str}");
        }
    }
}