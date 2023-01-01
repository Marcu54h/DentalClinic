using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.Services.AuthenticationService
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }

    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password,
            string confirmPassword);
        Task<User> Login(string username, string password);
    }
}
