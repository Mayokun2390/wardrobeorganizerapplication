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
using WardrobeOrganizerApp.Repositories.Interface;
using WardrobeOrganizerApp.Services.Interface;

namespace WardrobeOrganizerApp.Services.Implementation
{
    public class JWTSettingsService : IJWTSettingsService
    {
        private readonly JWTSettings _jWTSettings;
        private readonly IUserInterface _userInterface;
        private readonly IConfiguration _configuration;

        public JWTSettingsService(IOptions<JWTSettings> jwtsettings, IUserInterface userInterface, IConfiguration configuration)
        {
            _jWTSettings = jwtsettings.Value;
            _userInterface = userInterface;
            _configuration = configuration;
        }
      
        
        public string GenerateToken(User user)
       {
           var claims = new List<Claim> {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
           };

           foreach (var role in user.UserRoles)
           {
               claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
           }
           var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.SecurityKey));
           var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

           var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jWTSettings.Isseur,
               audience: _jWTSettings.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(_jWTSettings.ExpirationTime),
               signingCredentials: signingCredentials);
           return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
       }
        public async Task<User?> GetUserById(Guid id)
        {
            var user = await _userInterface.GetUserById(id);
            return user;
        }
    }

}