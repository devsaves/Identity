using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Helpers
{
    public class SimpleHelpers
    {
        public async Task SignInAsync(HttpContext httpContext,
        int id, string email
         )
        {
           await httpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme, Store2FA(id.ToString(), email));
        }

        public ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                    new Claim("sub", userId),
                    new Claim("amr", provider),
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}