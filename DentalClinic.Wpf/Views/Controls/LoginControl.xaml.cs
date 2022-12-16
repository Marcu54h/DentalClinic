
using System.Windows.Controls;
using Unity;


namespace DentalClinic.Wpf
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        [Dependency]
        public LoginControlViewModel ViewModel
        {
            set { DataContext = value; }
        }


        public LoginControl()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
