using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Authentication.Entities
{
    public class MyUser : IdentityUser<int>
    {
        public string CompanyId { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public string Group { get; set; } = "User";
     
    }
}