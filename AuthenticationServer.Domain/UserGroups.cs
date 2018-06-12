using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class UserGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        [ForeignKey("GroupId")]
        public int GroupId {get;set;}
        [Required]
        [ForeignKey("UserId")]
        public int UserId {get;set;}

        public Group Groups {get;set;}
        public User Users {get;set;}
    }
}