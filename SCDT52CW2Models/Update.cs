using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCDT52CW2Models
{
    public class Update
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UpdateType { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required, DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, Display(Name = "Action Notes")]
        public string Notes { get; set; }

        [Required, Display(Name = "Resolved?")]
        public bool isResolved { get; set; }

    }
}
