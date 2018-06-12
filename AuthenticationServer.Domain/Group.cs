using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string GroupName {get;set;}

        public ICollection<GroupPermissions> GroupsPermissions { get; set; }
        public ICollection<UserGroups> UsersGroups { get; set; }
    }
}