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
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Page
    {
        public Start()
        {
            InitializeComponent();
        }

        private void Start_Button_Click_1(object sender, RoutedEventArgs e)
        {
            string sensorPosition = SensorPosition_ComboBox.Text;
 
            int id;
            if (int.TryParse(ParticipantId_TextBox.Text, out id) && sensorPosition != "")
            {
                StudyManager manager = StudyManager.CreateInstance(id, sensorPosition);
                NavigationService.Navigate(manager.GetNextCondition());
            }
            else
            {
                MessageBox.Show("Please enter a participant ID and select a sensor position");
            }
        }
    }
}
