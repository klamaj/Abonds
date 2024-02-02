using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Services.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config, UserManager<UserModel> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
        }

        // Create user token
        public async Task<string> CreateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHndler = new JwtSecurityTokenHandler();

            var token = tokenHndler.CreateToken(tokenDescriptor);

            return tokenHndler.WriteToken(token);
        }
    }
}