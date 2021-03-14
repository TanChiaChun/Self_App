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

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoMyDay_Page.xaml
    /// </summary>
    public partial class TodoMyDay_Page : Page
    {
        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoMyDay_Page()
        {
            // Generic
            InitializeComponent();

            // Specific
            txtBlk_myDay.Text += DateTime.Now.ToString("d MMM");
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
    }
}
