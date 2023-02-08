using System.ComponentModel.DataAnnotations;

namespace Authentication.Dto
{
    public class ForgotPasswordDto
    {
        // [Required]
        // public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}