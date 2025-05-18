using Microsoft.AspNetCore.Mvc;
using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;
using Serilog;

namespace projectApi.BL
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDal AuthDal;
        public AuthService(IAuthDal authDal)
        {
            AuthDal = authDal ?? throw new ArgumentNullException(nameof(authDal));
        }

        public async Task<User> AuthenticateUser(LoginRequestDto loginRequest)
        {
            return await AuthDal.AuthenticateUser(loginRequest);
        }

        public string GenerateJwtToken(User user, int cartId, string role)
        {
           return AuthDal.GenerateJwtToken(user, cartId, user.Role);
        }

        public Task<string> Login(LoginRequestDto loginRequest)
        {
            return AuthDal.Login(loginRequest);
        }
    }
}
