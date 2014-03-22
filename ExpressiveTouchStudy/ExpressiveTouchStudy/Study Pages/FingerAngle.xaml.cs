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
    /// Interaction logic for FingerAngle.xaml
    /// </summary>
    public partial class FingerAngle : Page
    {
        private DateTime started;

        public FingerAngle()
        {
            InitializeComponent();

            this.Loaded += FingerAngle_Loaded;
            this.Task_Button.Click += Task_Button_Click;
        }

        private void Task_Button_Click(object sender, RoutedEventArgs e)
        {
            StudyManager manager = StudyManager.GetInstance();

            // TODO: get finger angle detected for the touch
            float fingerAngle = 0.0f;

            // TODO: work out if force within the correct range
            bool success = true;

            string[] data = new string[1];
            data[0] = fingerAngle.ToString();

            TimeSpan duration = DateTime.UtcNow - started;

            manager.LogTrial(duration, success, data);

            Page next = manager.GetNextCondition();
            NavigationService.Navigate(next);
        }

        private void FingerAngle_Loaded(object sender, RoutedEventArgs e)
        {
            string condition = StudyManager.GetInstance().Condition;

            string label = string.Format("Press the button with your finger at the following angle: {0}", condition);
            Instructions_Label.Text = label;

            started = DateTime.UtcNow;
        }
    }
}
