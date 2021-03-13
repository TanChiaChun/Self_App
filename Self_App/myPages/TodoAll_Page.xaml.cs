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
using Self_App.myWindows;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoAll_Page.xaml
    /// </summary>
    public partial class TodoAll_Page : Page
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Generic
        private Database db = new Database();

        // Specific
        private List<MyTask> tasks = new List<MyTask>();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoAll_Page()
        {
            // Generic
            InitializeComponent();

            // Specific
            tasks = db.Select_TodoAll();
            dataGrid_todoAll.ItemsSource = tasks;
        }
        
        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void dataGrid_todoAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid cDataGrid = e.Source as DataGrid;
            int i = cDataGrid.SelectedIndex;
            if (i == -1)
            {
                return;
            }

            TaskWindow taskWin = new TaskWindow(db.Select_Task(tasks[i].id));
            taskWin.ShowDialog();
        }
    }
}
