using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using projectApi.Models;
using projectApi.Models.DTO;
using LoginRequest = projectApi.Models.LoginRequest;

namespace projectApi.DAL
{
    public interface IAuthDal
    {
        Task<string> Login(LoginRequestDto loginRequest);
       string GenerateJwtToken(User user, int cartId,string role);
       Task<User> AuthenticateUser(LoginRequestDto loginRequest);
    }
}
