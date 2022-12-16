
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for ScheduleDayControl.xaml
    /// </summary>
    public partial class ScheduleDayControl : UserControl
    {
        [Dependency]
        public ScheduleDayControlViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }

        public ScheduleDayControl()
        {
            InitializeComponent();
        }
    }
}
