using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using projectApi.BL;
using projectApi.DAL;
using projectApi.Models;
using projectApi.Models.DTO;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace projectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;


        public AuthController(IAuthService AuthService)
        {
            authService = AuthService ?? throw new ArgumentNullException(nameof(AuthService));

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            

            try
            {
                var token=await authService.Login(loginRequest);
                Log.Information("User Login And Created Him Currently Cart");
                return Ok(new { token });

            }
            catch (KeyNotFoundException ex)
            {
                Log.Error($"שגיאה לכניסה");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error($"Error: {ex.Message}");
                throw;
            }

        }

    } 
}
