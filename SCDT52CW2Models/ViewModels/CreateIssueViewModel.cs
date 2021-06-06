using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDT52CW2Models.ViewModels
{
    public class CreateIssueViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string Author { get; set; }

        public string Desc { get; set; }

        public List<Update> Actions { get; set; }
        public bool isClosed { get; set; }
        //Below Variables apply for technical issues 
        public bool isTechnical { get; set; }
        public IEnumerable<SelectListItem> AssetsSelect { get; set; }
    }
}
