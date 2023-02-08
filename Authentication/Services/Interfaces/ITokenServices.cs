using System;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Services.Operations
{
    public interface ITokenServices
    {
        Task<UserToken> GenerateToken(MyUser user);
    }
}
