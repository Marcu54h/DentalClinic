using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DentalClinic.WpfMD.Controls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PswdProperty =
            DependencyProperty.Register("Pswd", typeof(string), typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PswdPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private bool _pswdIsChanging;

        private static void PswdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox pswdBox)
            {
                pswdBox.UpdatePswd();
            }
        }

        public string Pswd
        {
            get => (string)GetValue(PswdProperty);
            set => SetValue(PswdProperty, value);
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _pswdIsChanging = true;
            Pswd = _pswdBox.Password;
            _pswdIsChanging = false;
        }

        private void UpdatePswd()
        {
            if(!_pswdIsChanging)
            {
                _pswdBox.Password = Pswd;
            }
        }
    }
}
