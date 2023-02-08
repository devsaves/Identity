using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Entities;
using Authentication.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Services.Operations
{
    public class RolesManagerServices : IRolesManagerServices
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RolesManagerServices(
        UserManager<MyUser> userManager,
        RoleManager<Role> roleManager
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRole(RoleDto role)
        {
            var result = await _roleManager.CreateAsync(new Role { Name = role.Name });
            return result;
        }
        public async Task<string> UpdateUserRoles(UpdateUserRoleDto model)
        {
            var myUser = await _userManager.FindByNameAsync(model.UserName);

            if (myUser == null) throw new Exception("Usuário era nulo.");

            if (model.Delete)
            {

                await _userManager.RemoveFromRoleAsync(myUser, model.Role);
                return "Role removed";
            }
            else
            {
                await _userManager.AddToRoleAsync(myUser, model.Role);
                return "Role Added";
            }

            throw new Exception("Erro desconhecido.");

        }
        public async Task<IList<string>> GetRoles(MyUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            return role;
        }



    }
}