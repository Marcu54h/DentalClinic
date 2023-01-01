using DentalClinic.WpfMD.Services.AuthenticationService;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.State.Authenticators
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }

        Task<RegistrationResult> Register(string email, string password, string confirmPassword);
        Task<bool> Login(string username, string password);
        bool Logout();
    }
}
