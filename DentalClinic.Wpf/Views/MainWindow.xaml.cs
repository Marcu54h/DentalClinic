using System.Windows;
using Unity;

namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 'Dependency' annotation causes <see cref="MainWindowViewModel"/> class to inject in place of 'value'.
        /// </summary>
        [Dependency]
        public MainWindowViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
