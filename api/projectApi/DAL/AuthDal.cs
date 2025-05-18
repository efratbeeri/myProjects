using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using projectApi.BL;
using projectApi.Models;
using projectApi.Models.DTO;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projectApi.DAL
{
    public class AuthDal : IAuthDal
    {
        private readonly IConfiguration _config;
        private readonly OrdersDBContex ordersDBContex;
        private readonly ICartService _cartService;
        private readonly IMapper mapper;
        private readonly ILogger<AuthDal> Log;

        public AuthDal(IConfiguration config, OrdersDBContex ordersDBContex, ICartService cartService, IMapper mapper, ILogger<AuthDal> log)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            this.ordersDBContex = ordersDBContex ?? throw new ArgumentNullException(nameof(ordersDBContex));
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<User> AuthenticateUser(LoginRequestDto loginRequest)
        {
            // חפש את המשתמש במסד הנתונים לפי שם
            var user = await ordersDBContex.User
                .SingleOrDefaultAsync(u => u.Name == loginRequest.Username);

            // אם המשתמש לא נמצא, חזור null
            if (user == null)
                return null;

            // בדוק אם הסיסמה שסופקה תואמת את הסיסמה המוצפנת במסד הנתונים
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if (!passwordMatch)
                return null;

            // החזר את המשתמש אם האימות הצליח
            return user;
        }

        public string GenerateJwtToken(User user, int cartId,string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim("CartId", cartId.ToString()),
                new Claim("Role", role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(LoginRequestDto loginRequest)
        {
            var user = await AuthenticateUser(loginRequest);

            if (user == null)
                return "שם משתמש או סיסמה לא נכונים!";

            Log.LogInformation("user.id: " + user.Id);


            var cartDto = new CartDto
            {
                UserId = user.Id,
                Final = false
            };

            if (cartDto == null)
            {
                return "CartDto is null.";
            }
            if (cartDto.UserId <= 0)
            {
                Log.LogError("Invalid UserId for cart creation.");
                Log.LogInformation("Failed to create cart: Invalid UserId.");
            }


            try
            {
                var cartId = await _cartService.Add(cartDto);
                var createdCart = await ordersDBContex.Cart.FirstOrDefaultAsync(c => c.Id == cartId);
                if (createdCart == null || createdCart.UserId != user.Id)
                {
                    Log.LogInformation("Cart creation failed or not linked to user.");
                }

                if (cartId <= 0)
                {
                    Log.LogInformation("Failed to create cart.");
                }
                var token = GenerateJwtToken(user, cartId,user.Role);
                if (string.IsNullOrEmpty(token))
                {
                    Log.LogInformation("Failed to generate token.");
                }

                return JsonConvert.SerializeObject(new { token = GenerateJwtToken(user, cartId, user.Role) });

            }
            catch (Exception ex)
            {
                return $"Internal server error: {ex.Message}";
            }
        }

    }
}
