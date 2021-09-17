using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SantafeApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<SantafeApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<SantafeApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(SantafeApiUser user)
        {

            _ = user ?? throw new ArgumentException("Parameter user cannot be null");

            var role = await _userManager.GetRolesAsync(user);
            var identityOptions = new IdentityOptions();
            var userRole = role.FirstOrDefault(e => e.ToLower() == "admin" || e.ToLower() == "customer");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Identity:Key"]);

            var codCliente = 0;
            if (user.CodCliente.HasValue)
                codCliente = user.CodCliente.Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId", user.Id),
                        new Claim("ClientId", codCliente.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
