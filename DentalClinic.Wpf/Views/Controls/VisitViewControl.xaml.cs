
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for VisitViewControl.xaml
    /// </summary>
    public partial class VisitViewControl : UserControl
    {

        [Dependency]
        public VisitViewControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public VisitViewControl()
        {
            InitializeComponent();
        }
    }
}
