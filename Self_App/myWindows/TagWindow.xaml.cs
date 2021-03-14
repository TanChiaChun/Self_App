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
using System.Windows.Shapes;
using Self_App.myClasses;

namespace Self_App.myWindows
{
    /// <summary>
    /// Interaction logic for TagWindow.xaml
    /// </summary>
    public partial class TagWindow : Window
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Generic
        private MyCls f = new MyCls();
        private Database db = new Database();

        // Specific
        private HashSet<string> tags = new HashSet<string>();
        public bool toAdd { get; private set; } = false;
        public string tag { get; private set; } = "";

        public TagWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            tags = db.Select_Tags();
            cmBx_tag.ItemsSource = tags;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void AddTag()
        {
            if (!f.IsTextInputValid(false, cmBx_tag.Text, "Tag"))
            {
                return;
            }

            tag = cmBx_tag.Text;
            toAdd = true;
            Close();
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
        // Generic with differences
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddTag();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        // Specific
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            AddTag();
        }
    }
}
