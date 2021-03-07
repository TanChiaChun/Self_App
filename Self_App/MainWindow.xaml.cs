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
using Self_App.myWindows;

namespace Self_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDarkMode = false;
        
        public MainWindow()
        {
            InitializeComponent();
        }

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
    }
}
