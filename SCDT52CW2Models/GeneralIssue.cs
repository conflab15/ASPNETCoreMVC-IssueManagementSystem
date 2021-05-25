using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCDT52CW2Models
{
    public class GeneralIssue
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Issue ID")]
        public string IssueID { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, Display(Name ="Issue Description")]
        public string Desc { get; set; }

        [Required, Display(Name = "Actions")]
        public List<Update> Actions { get; set; }

        [Required]
        public bool isClosed { get; set; }

    }
}
