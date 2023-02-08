using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public MyUser MyUser { get; set; }
        public Role Role { get; set; }
    }
}