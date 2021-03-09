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
    /// Interaction logic for StepWindow.xaml
    /// </summary>
    public partial class StepWindow : Window
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Generic
        private MyFunctions f = new MyFunctions();

        // Specific
        public bool toAdd { get; private set; } = false;
        public bool toDelete { get; private set; } = false;
        public Tuple<bool, string> step { get; private set; }

        public StepWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Add";
            btn_update.Visibility = Visibility.Collapsed;
            btn_delete.Visibility = Visibility.Collapsed;
        }

        public StepWindow(Tuple<bool, string> step)
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Modify";
            btn_add.Visibility = Visibility.Collapsed;
            txtBx_step.Text = step.Item2;
            chkBx_step.IsChecked = step.Item1;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void AddStep()
        {
            if (!f.IsTextInputValid(false, txtBx_step.Text, "Step"))
            {
                return;
            }

            step = new Tuple<bool, string>((bool)chkBx_step.IsChecked, txtBx_step.Text);
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
                AddStep();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        // Specific
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            AddStep();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            toDelete = true;
            Close();
        }
    }
}
