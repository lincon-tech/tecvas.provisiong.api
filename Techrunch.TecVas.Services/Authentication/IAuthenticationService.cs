using Techrunch.TecVas.Entities;

namespace Techrunch.TecVas.Services.Authentication
{
    public interface IAuthenticationService
    {
        User Authenticate(UserLogin userLogin);
        public string GenerateToken(User userModel);
    }
}