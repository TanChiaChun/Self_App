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
using Self_App.myClasses;

namespace Self_App.myPages
{
    /// <summary>
    /// Interaction logic for TodoProject_Page.xaml
    /// </summary>
    public partial class TodoProject_Page : Page, ITodo
    {
        //////////////////////////////////////////////////
        // Class variables
        //////////////////////////////////////////////////
        // Specific
        private string project = "";
        public StackPanel stkPnl_proj { get; }

        //////////////////////////////////////////////////
        // Main
        //////////////////////////////////////////////////
        public TodoProject_Page()
        {
            // Generic
            InitializeComponent();
        }

        //////////////////////////////////////////////////
        // Functions
        //////////////////////////////////////////////////
        public void RefreshData()
        {
            UpdateSections();
        }

        public void UpdateProject(string pProject)
        {
            project = pProject;
            txtBlk_proj.Text = project;
        }

        public void UpdateSections()
        {
            stkPnl_sect.Children.Clear();
            List<string> sections = Db.Select_Sections(project);
            foreach (string section in sections)
            {
                StackPanel stkPnl = new StackPanel();
                stkPnl.Children.Add(new TextBlock(new Run(section)));
                stkPnl_sect.Children.Add(stkPnl);
            }
        }

        //////////////////////////////////////////////////
        // Events
        //////////////////////////////////////////////////
    }
}
