using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalClinic.WpfMD.Abstraction;
using DentalClinic.WpfMD.Models;
using DentalClinic.WpfMD.Services.AuthenticationService;
using System;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.ViewModels
{
    public partial class LoginViewModel : ObservableRecipient, IViewType
    {
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private string userLogin = string.Empty;
        private IAuthenticationService _authenticationService = default!;

        public ViewType ViewType => ViewType.LoginView;

        [RelayCommand]
        private async Task Login()
        {
            _authenticationService = App.GetService<IAuthenticationService>();
            await _authenticationService.Login(UserLogin, Password);
        }

    }
}
