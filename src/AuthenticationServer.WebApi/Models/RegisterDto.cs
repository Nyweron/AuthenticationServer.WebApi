using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AuthenticationServer.WebApi.Models
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        public string Role { get; set; }

        [JsonConstructor]
        public RegisterDto(string email, string password, string role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }
}