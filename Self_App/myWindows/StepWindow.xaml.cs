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
        // Specific
        private MyWrite type;
        public bool toAdd { get; private set; } = false;
        public bool toUpdate { get; private set; } = false;
        public bool toDelete { get; private set; } = false;
        public Tuple<bool, string> step { get; private set; }
        private enum MyWrite
        {
            Add,
            Update
        }

        public StepWindow()
        {
            // Generic
            InitializeComponent();

            // Specific
            type = MyWrite.Add;
            this.Title += $" - {type}";
            btn_update.Visibility = Visibility.Collapsed;
            btn_delete.Visibility = Visibility.Collapsed;
        }

        public StepWindow(Tuple<bool, string> step)
        {
            // Generic
            InitializeComponent();

            // Specific
            type = MyWrite.Update;
            this.Title += $" - {type}";
            btn_add.Visibility = Visibility.Collapsed;
            txtBx_step.Text = step.Item2;
            chkBx_step.IsChecked = step.Item1;
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        private void WriteStep()
        {
            if (!MyCls.IsTextInputValid(false, txtBx_step.Text, "Step"))
            {
                return;
            }

            step = new Tuple<bool, string>((bool)chkBx_step.IsChecked, txtBx_step.Text);

            if (type == MyWrite.Add)
            {
                toAdd = true;
            }
            else if (type == MyWrite.Update)
            {
                toUpdate = true;
            }
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
                WriteStep();
            }
            else if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        // Specific
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            WriteStep();
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            WriteStep();
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Delete step?", "Delete Step", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    toDelete = true;
                    Close();
                    break;
            }
        }
    }
}
