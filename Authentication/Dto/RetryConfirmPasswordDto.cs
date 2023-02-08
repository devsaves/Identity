using System.ComponentModel.DataAnnotations;

namespace Authentication.Dto
{
    public class RetryConfirmPasswordDto
    {
        // [Required]
        // public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}