
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewPatientControl.xaml
    /// </summary>
    public partial class EditPatientControl : UserControl
    {
        [Dependency]
        public EditPatientControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public EditPatientControl()
        {
            InitializeComponent();
        }
    }
}
