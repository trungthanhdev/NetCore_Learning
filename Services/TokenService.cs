using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Models;

namespace NetCore_Learning.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key, _refresh_key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
            _refresh_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefreshKey"]!));
        }

        public string CreateToken(AppUser user, string role, bool isAccess)
        {
            //payload
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? ""),
                new Claim(ClaimTypes.Role, role)
            };
            //ki
            var creds = isAccess ? new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature)
                     : new SigningCredentials(_refresh_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = isAccess ? new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            }
            : new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}