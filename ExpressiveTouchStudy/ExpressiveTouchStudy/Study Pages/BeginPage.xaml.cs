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

namespace ExpressiveTouchStudy.Study_Pages
{
    /// <summary>
    /// Interaction logic for BeginPage.xaml
    /// </summary>
    public partial class BeginPage : Page
    {
        public BeginPage()
        {
            InitializeComponent();

            this.Loaded += BeginPage_Loaded;
            this.Begin_Button.Click += Begin_Button_Click;
        }

        private void BeginPage_Loaded(object sender, RoutedEventArgs e)
        {
            string name = StudyManager.GetInstance().GetCurrentTechnique().name;
            string label = string.Format("You are about to do the {0} technique. Press the begin button when you are ready to start", name);

            Title_TextBlock.Text = name;
            Begin_TextBlock.Text = label;
        }

        private void Begin_Button_Click(object sender, RoutedEventArgs e)
        {
            Page next = StudyManager.GetInstance().GetNextCondition();
            NavigationService.Navigate(next);
        }
    }
}
