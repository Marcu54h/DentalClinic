
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewPatientControl.xaml
    /// </summary>
    public partial class AddTreatmentControl : UserControl
    {
        [Dependency]
        public AddTreatmentControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public AddTreatmentControl()
        {
            InitializeComponent();
        }
    }
}
