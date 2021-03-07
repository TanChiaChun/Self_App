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
using Self_App.myWindows;
using Self_App.myPages;

namespace Self_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private bool isDarkMode = false;
        private TodoPage todoPg = new TodoPage();

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public MainWindow()
        {
            // Generic
            InitializeComponent();
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_colorMode_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            
            string colorMode = "Light";
            if (!isDarkMode)
            {
                isDarkMode = true;
                colorMode = "Dark";
            }
            else if (isDarkMode)
            {
                isDarkMode = false;
            }

            App.Current.Resources.MergedDictionaries[0].Source = new Uri($"themes/{colorMode}.xaml", UriKind.Relative);
            btn.Content = colorMode;
        }

        private void btn_todo_Click(object sender, RoutedEventArgs e)
        {
            fr_main.Content = todoPg;
        }
    }
}
