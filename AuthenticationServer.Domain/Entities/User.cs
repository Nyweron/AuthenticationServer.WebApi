using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }

        public ICollection<UserAuthToken> UsersAuthTokens { get; set; }
        public ICollection<Password> Passwords { get; set; }
        public ICollection<UserGroup> UsersGroups { get; set; }
    }
}