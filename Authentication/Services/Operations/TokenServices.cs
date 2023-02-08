using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Authentication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Services.Operations
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly RolesManagerServices _rolesManagerServices;
        public TokenServices(IConfiguration configuration,
                    RolesManagerServices rolesManagerServices
        )
        {
            _configuration = configuration;
            _rolesManagerServices = rolesManagerServices;
        }
        public async Task<UserToken> GenerateToken(MyUser user)
        {

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.UserName),
            };

            var roles = await _rolesManagerServices.GetRoles(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));

            var TokenHandler = new JwtSecurityTokenHandler();

            var ExpDate = DateTime.Now.AddHours(Double.Parse(_configuration["TOKEN:EXPIRESHOURS"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = ExpDate,
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256),
            };

            SecurityToken token = TokenHandler.CreateToken(tokenDescriptor);

            var usrTkn = new UserToken()
            {
                Authenticated = true,
                Expiration = tokenDescriptor.Expires ?? DateTime.Now,
                Token = TokenHandler.WriteToken(token),
                UserName = user.UserName
            };

            return usrTkn;
        }

    }
}
