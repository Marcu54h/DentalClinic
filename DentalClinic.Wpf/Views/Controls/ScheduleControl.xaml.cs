
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for ScheduleControl.xaml
    /// </summary>
    public partial class ScheduleControl : UserControl
    {
        [Dependency]
        public ScheduleControlViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }

        public ScheduleControl()
        {
            InitializeComponent();
            
        }
    }
}
