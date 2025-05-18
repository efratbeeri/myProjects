using Microsoft.AspNetCore.Mvc;
using projectApi.Models;
using projectApi.Models.DTO;

namespace projectApi.BL
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequestDto loginRequest);
        string GenerateJwtToken(User user, int cartId, string role);
        Task<User> AuthenticateUser(LoginRequestDto loginRequest);
    }
}
