
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewPatientControl.xaml
    /// </summary>
    public partial class AddPatientControl : UserControl
    {
        [Dependency]
        public AddPatientControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public AddPatientControl()
        {
            InitializeComponent();
        }
    }
}
