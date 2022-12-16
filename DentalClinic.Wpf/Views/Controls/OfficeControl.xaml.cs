
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewPatientControl.xaml
    /// </summary>
    public partial class OfficeControl : UserControl
    {
        [Dependency]
        public OfficeControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public OfficeControl()
        {
            InitializeComponent();
        }
    }
}
