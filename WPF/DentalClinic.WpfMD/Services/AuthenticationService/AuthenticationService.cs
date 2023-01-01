using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel;

namespace DentalClinic.WpfMD.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}
