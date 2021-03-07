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
        public bool hasChange { get; private set; } = false;
        public Tuple<bool, string> step { get; private set; }

        public StepWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            this.Title += " - Add";
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
            hasChange = true;
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
    }
}
