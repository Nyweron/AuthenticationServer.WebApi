using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.WebApi.Models
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}