using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCDT52CW2Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, Display(Name = "Created by:")]
        public string Author { get; set; }

        [Required, Display(Name = "Issue Description")]
        public string Desc { get; set; }

        [Required, Display(Name = "Actions")]
        public List<Update> Actions { get; set; }

        [Required]
        public bool isClosed { get; set; }

        //Below Variables apply for technical issues 
        [Required, Display(Name = "Technical Issue?")]
        public bool isTechnical { get; set; }

        [Display(Name = "Assets Affected")]
        public int AffectedAsset { get; set; }

        [ForeignKey("AffectedAsset")]
        public Assets Asset{ get; set; }

        public Issue()
        {
            Actions = new List<Update>();
        }
    }
}