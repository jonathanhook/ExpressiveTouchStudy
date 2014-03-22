using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpressiveTouchStudy
{
    /// <summary>
    /// Interaction logic for QuiveringFinger.xaml
    /// </summary>
    public partial class QuiveringFinger : Page
    {
        public DateTime started;

        public QuiveringFinger()
        {
            InitializeComponent();

            this.Loaded += QuiveringFinger_Loaded;
            this.Task_Button.PreviewMouseDown += Task_Button_MouseDown;
        }


        private void Task_Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO: this one isn't implemented
            // start measuring the quiver gesture in some way

            Page next = StudyManager.GetInstance().GetNextCondition();
            NavigationService.Navigate(next);
        }


        private void QuiveringFinger_Loaded(object sender, RoutedEventArgs e)
        {
            string condition = StudyManager.GetInstance().Condition;

            string label = "Press and hold the button and quiver your finger for three seconds";
            Instructions_Label.Text = label;

            started = DateTime.UtcNow;
        }
    }
}
