using DentalClinic.Data;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace DentalClinic.Wpf
{
    public class LoginControlViewModel : BaseViewModel
    {
        protected string hide = "malitek2006";

        private bool loginAllowed = false;

        private ICommand commandToLogin;

        private string userPassword;

        public bool LoginAllowed { get { return loginAllowed; } set { loginAllowed = value; NotifyPropertyChanged(nameof(LoginAllowed)); } }

        public string UserLogin { get; set; }

        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                if (userPassword == hide)
                {
                    LoginAllowed = true;
                }
                else
                    LoginAllowed = false;
            }
        }

        public ICommand CommandToLogin
        {
            get
            {
                if (commandToLogin is null)
                    commandToLogin = new ActionCommand(x =>
                    {

                        try
                        {
                            MainDataContext.MainContext.People.First();
                        }
                        catch
                        {
                            App.container.Resolve<IKnowWhoCall>().Call<SettingControl, LoginControl>();
                            return;
                        }

                        App.container.Resolve<MainWindowViewModel>().blok = true;
                        App.container.Resolve<IDisplayControls>().DisplayControl(App.container.Resolve<ScheduleControl>());
                    });
                        
                return commandToLogin;
            }
        }


        private SecureString toSS(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
    }
}
