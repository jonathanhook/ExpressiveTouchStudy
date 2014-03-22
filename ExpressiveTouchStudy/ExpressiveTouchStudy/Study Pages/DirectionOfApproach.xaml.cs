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

namespace ExpressiveTouchStudy
{
    /// <summary>
    /// Interaction logic for DirectionOfApproach.xaml
    /// </summary>
    public partial class DirectionOfApproach : Page
    {
        public DateTime started;

        public DirectionOfApproach()
        {
            InitializeComponent();

            this.Loaded += DirectionOfApproach_Loaded;
            this.Task_Button.Click += Task_Button_Click;
        }

        private void Task_Button_Click(object sender, RoutedEventArgs e)
        {
            StudyManager manager = StudyManager.GetInstance();

            // TODO: get direction detected for the touch
            float direction = 0.0f;

            // TODO: work out if force within the correct range
            bool success = true;

            string[] data = new string[1];
            data[0] = direction.ToString();

            TimeSpan duration = DateTime.UtcNow - started;

            manager.LogTrial(duration, success, data);

            Page next = manager.GetNextCondition();
            NavigationService.Navigate(next);
        }

        private void DirectionOfApproach_Loaded(object sender, RoutedEventArgs e)
        {
            string condition = StudyManager.GetInstance().Condition;

            string label = string.Format("Press the button, approaching form the following direction: {0}", condition);
            Instructions_Label.Text = label;

            started = DateTime.UtcNow;
        }
    }
}
