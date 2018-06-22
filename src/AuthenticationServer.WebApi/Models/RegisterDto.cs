using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.WebApi.Models
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}