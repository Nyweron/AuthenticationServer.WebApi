using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class UserAuthTokens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public int UserId {get;set;}
        public int AuthTokenId {get;set;}

        public User Users {get;set;}
        public AuthToken AuthTokens{get;set;}
    }
}