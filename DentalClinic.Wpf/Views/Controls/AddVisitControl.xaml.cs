
using System.Windows.Controls;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for AddVisit.xaml
    /// </summary>
    public partial class AddVisitControl : UserControl
    {
        [Dependency]
        public AddVisitControlViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public AddVisitControl()
        {
            InitializeComponent();
        }
    }
}
