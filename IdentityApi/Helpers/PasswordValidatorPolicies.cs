using System.Reflection;
using System.Threading.Tasks;
using IdentityApi.Respository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Helpers
{
    public class PasswordValidatorPolicies<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var userName = await manager.GetUserNameAsync(user);

            if (userName == password)
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "Usuario não pode ser igual a senha." }
                );
            }
            if (password.ToUpper().Contains("PASSWORD"))
            {
                return IdentityResult.Failed(
                    new IdentityError { Description = "Usuario não pode ser igual a senha." }
                );
            }
            return IdentityResult.Success;
        }
    }
}