using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.Domain
{
    public class Password
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Pwd { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public bool ResetEmailSent { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string PasswordQuestion { get; set; }
        [Required]
        [MaxLength(60)]
        public string PasswordAnswer { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId {get;set;}
        public User Users {get;set;}
    }
}