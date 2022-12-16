
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for TreatmentControl.xaml
    /// </summary>
    public partial class PriceListControl : UserControl
    {
        [Dependency]
        public PriceListControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public PriceListControl()
        {
            InitializeComponent();
        }

       
    }
}
