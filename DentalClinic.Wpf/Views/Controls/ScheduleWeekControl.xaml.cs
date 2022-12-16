
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for ScheduleControl.xaml
    /// </summary>
    public partial class ScheduleWeekControl : UserControl
    {
        [Dependency]
        public ScheduleWeekControlViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }

        public ScheduleWeekControl()
        {
            InitializeComponent();
            
        }
    }
}
