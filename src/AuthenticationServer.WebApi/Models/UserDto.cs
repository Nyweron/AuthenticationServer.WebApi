using System;

namespace AuthenticationServer.WebApi.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}