using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.WebApi.Entities
{
    public class GroupPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        [Required]
        [ForeignKey("GroupId")]
        public int GroupId {get;set;}
        [Required]
        [ForeignKey("PermissionId")]
        public int PermissionId {get;set;}

        public Group Groups {get;set;}
        public Permission Permissions{get;set;}
    }
}