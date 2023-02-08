using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services.Interface
{
    public interface IRolesManagerServices
    {
        Task<IdentityResult> CreateRole(RoleDto role);
        Task<string> UpdateUserRoles(UpdateUserRoleDto model);
        Task<IList<string>> GetRoles(MyUser user);
    }
}