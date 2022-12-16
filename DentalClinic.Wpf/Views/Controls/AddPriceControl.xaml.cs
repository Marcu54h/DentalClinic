
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddNewPatientControl.xaml
    /// </summary>
    public partial class AddPriceControl : UserControl
    {
        [Dependency]
        public AddPriceControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public AddPriceControl()
        {
            InitializeComponent();
        }
    }
}
