using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastLogin { get; set; }

        [ForeignKey("UserAuthTokens")]
        public UserAuthTokens UsersAuthTokens { get; set; }
        public int UsersAuthTokensId { get; set; }
    }
}
