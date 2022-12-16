
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for FilesControl.xaml
    /// </summary>
    public partial class EmployeeControl : UserControl
    {

        [Dependency]
        public EmployeeControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public EmployeeControl()
        {
            InitializeComponent();
        }
    }
}
