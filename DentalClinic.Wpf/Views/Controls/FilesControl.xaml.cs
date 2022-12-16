
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for FilesControl.xaml
    /// </summary>
    public partial class FilesControl : UserControl
    {

        [Dependency]
        public FilesControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public FilesControl()
        {
            InitializeComponent();
        }

        private void PatientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
