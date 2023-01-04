using System.Windows.Controls;
using System.Windows.Input;

namespace DentalClinic.WpfMD.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        

        public LoginView()
        {
            InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _loginButton.Command.Execute(this);
            }
        }
    }
}
