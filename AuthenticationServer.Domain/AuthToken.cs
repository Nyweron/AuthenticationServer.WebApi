using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class AuthToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public ICollection<UserAuthTokens> UsersAuthTokens{get;set;}
    }
}