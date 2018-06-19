using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.WebApi.Entities
{
    public class UserAuthToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        [ForeignKey("UserId")]
        public int UserId {get;set;}
        [Required]
        [ForeignKey("AuthTokenId")]
        public int AuthTokenId {get;set;}

        public User Users {get;set;}
        public AuthToken AuthTokens{get;set;}
    }
}