using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Services.Operations;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolesManagerServices _rolesManagerServices;
        public RolesController(RolesManagerServices RolesManagerServices)
        {
            _rolesManagerServices = RolesManagerServices;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(RoleDto role)
        {
            var result = await _rolesManagerServices.CreateRole(role);
            return Ok(result);
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleDto model)
        {
            var result = await _rolesManagerServices.UpdateUserRoles(model);
            return Ok(result);
        }
    }


}
