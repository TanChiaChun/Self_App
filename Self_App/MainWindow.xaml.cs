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
using System.Configuration;
using Self_App.myWindows;
using Self_App.myClasses;
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
        private string myColorMode = ConfigurationManager.AppSettings.Get("ColorMode");
        private TodoPage todoPg;
        
        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public MainWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            SetColorMode();
            Db.InitDb();

            DateTime calLastChk = Db.Select_Hour("W_Calendar");
            MyCls.ProcessDateTextBlock(txtBlk_cal, calLastChk, DateTime.Today, DateTime.Today.AddDays(-1), MyCls.DATE_FORMAT_TIME_DATE, "Calendar last refresh: ");
            todoPg = new TodoPage(calLastChk, txtBlk_cal);
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void SetColorMode()
        {
            App.Current.Resources.MergedDictionaries[0].Source = new Uri($"themes/{myColorMode}.xaml", UriKind.Relative);
            btn_colorMode.Content = myColorMode;
        }
        
        private void SetConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        private void btn_colorMode_Click(object sender, RoutedEventArgs e)
        {
            if (myColorMode == MyCls.ColorMode.Light.ToString())
            {
                myColorMode = MyCls.ColorMode.Dark.ToString();
                SetConfig("ColorMode", myColorMode);
            }
            else if (myColorMode == MyCls.ColorMode.Dark.ToString())
            {
                myColorMode = MyCls.ColorMode.Light.ToString();
                SetConfig("ColorMode", myColorMode);
            }

            SetColorMode();
        }

        private void btn_todo_Click(object sender, RoutedEventArgs e)
        {
            todoPg.RefreshData();
            fr_main.Content = todoPg;
        }
    }
}
