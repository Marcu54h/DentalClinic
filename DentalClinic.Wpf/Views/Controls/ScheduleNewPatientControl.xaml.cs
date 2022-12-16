
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddVisit.xaml
    /// </summary>
    public partial class ScheduleNewPatientControl : UserControl
    {
        [Dependency]
        public ScheduleNewPatientControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public ScheduleNewPatientControl()
        {
            InitializeComponent();
        }
    }
}
