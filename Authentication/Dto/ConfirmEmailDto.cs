using System.ComponentModel.DataAnnotations;

namespace Authentication.Dto
{
    public class ConfirmEmailDto
    {
        [Required]
        public string Token { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}