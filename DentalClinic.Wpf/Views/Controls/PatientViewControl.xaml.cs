
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for PatientViewControl.xaml
    /// </summary>
    public partial class PatientViewControl : UserControl
    {
        [Dependency]
        public PatientViewControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public PatientViewControl()
        {
            InitializeComponent();
        }
    }
}
