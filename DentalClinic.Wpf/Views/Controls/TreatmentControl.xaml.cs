using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for TreatmentControl.xaml
    /// </summary>
    public partial class TreatmentControl : UserControl
    {
        [Dependency]
        public TreatmentControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public TreatmentControl()
        {
            InitializeComponent();
        }

       
    }
}
