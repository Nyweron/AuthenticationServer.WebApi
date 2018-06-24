using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.WebApi.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The Email must be submitted", AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Password must be submitted", AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Password { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "The FirstName must be submitted", AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName must be submitted", AllowEmptyStrings = false)]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Login must be submitted", AllowEmptyStrings = false)]
        [MaxLength(20)]
        public string Login { get; set; }
    }
}