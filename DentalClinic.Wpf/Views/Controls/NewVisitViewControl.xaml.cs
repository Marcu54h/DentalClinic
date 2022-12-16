
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for VisitViewControl.xaml
    /// </summary>
    public partial class NewVisitViewControl : UserControl
    {

        [Dependency]
        public NewVisitViewControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public NewVisitViewControl()
        {
            InitializeComponent();
        }
    }
}
