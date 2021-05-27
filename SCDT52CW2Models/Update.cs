using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCDT52CW2Models
{
    public class Update
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string Author { get; set; }

        [Required] //Foreign Key
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        [Required, Display(Name = "Action Notes")]
        public string Notes { get; set; }

        [Required, Display(Name = "Resolved?")]
        public bool isResolved { get; set; }

        [Required]
        public int IssueID { get; set; }
        

    }
}
