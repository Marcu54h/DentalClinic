
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for VisitControl.xaml
    /// </summary>
    public partial class VisitControl : UserControl
    {
        [Dependency]
        public VisitControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public VisitControl()
        {
            InitializeComponent();
        }
    }
}
