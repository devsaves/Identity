using System.ComponentModel.DataAnnotations;

namespace Authentication.Dto
{
    public class T2FactorDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Token { get; set; }
    }
}