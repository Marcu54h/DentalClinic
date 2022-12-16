
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddEmployeeControl.xaml
    /// </summary>
    public partial class AddEmployeeControl : UserControl
    {
        [Dependency]
        public AddEmployeeControlViewModel ViewModel
        {
            set { DataContext = value; }
        }
            

        public AddEmployeeControl()
        {
            InitializeComponent();
        }
    }
}
