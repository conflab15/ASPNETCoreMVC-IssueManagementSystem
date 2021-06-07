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
        public Issue Issue { get; set; }
        public IEnumerable<SelectListItem> AssetsSelect { get; set; }
    }
}
