using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.WebApi.Entities
{
    public class AuthToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        [MaxLength(256)]
        public string Token { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public ICollection<UserAuthToken> UsersAuthTokens{get;set;}
    }
}