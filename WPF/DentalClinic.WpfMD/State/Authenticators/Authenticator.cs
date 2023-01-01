using DentalClinic.WpfMD.Services.AuthenticationService;
using System;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authService;

        public Authenticator(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public User CurrentUser { get; private set; } = User.Empty;

        public bool IsLoggedIn => CurrentUser is not null;

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                CurrentUser = await _authService.Login(username, password);
            }
            catch (Exception)
            {

            }

            return CurrentUser is not null;
        }

        public bool Logout()
        {
            CurrentUser = User.Empty;
            return true;
        }

        public async Task<RegistrationResult> Register(string email, string password, string confirmPassword)
        {
            return await _authService.Register(email, email.Split('@')[0], password, confirmPassword);
        }
    }
}
