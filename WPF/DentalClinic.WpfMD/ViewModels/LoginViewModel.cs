using CommunityToolkit.Mvvm.ComponentModel;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;

namespace DentalClinic.WpfMD.ViewModels
{
    public partial class LoginViewModel : ObservableRecipient, IViewType
    {
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private string login = string.Empty;
        public ViewType ViewType => ViewType.LoginView;
        
       
        public LoginViewModel()
        { 
        }
    }
}
