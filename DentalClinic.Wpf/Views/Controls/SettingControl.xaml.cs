
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for SettingControl.xaml
    /// </summary>
    public partial class SettingControl : UserControl
    {
        [Dependency]
        public SettingControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public SettingControl()
        {
            InitializeComponent();
        }
    }
}
