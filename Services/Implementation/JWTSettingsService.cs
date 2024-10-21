using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WardrobeOrganizerApp.AuthenticationSettings;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class JWTSettingsService : IJWTSettingsService
    {
        private readonly JWTSettings _jWTSettings;

        public JWTSettingsService(IOptions<JWTSettings> jwtsettings)
        {
            _jWTSettings = jwtsettings.Value;
        }
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                
            };
            foreach (var role in user.UserRoles)
            {
                claims.Add(new Claim("role" , role.Role.Name));
             
            }

               var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.SecurityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtecurityToken = new JwtSecurityToken(
                    issuer: _jWTSettings.Isseur,
                    audience : _jWTSettings.Audience,
                    claims : claims,
                    expires : DateTime.UtcNow.AddMinutes(_jWTSettings.ExpiratinTime),
                    signingCredentials: signingCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(jwtecurityToken);
        }
    }
}