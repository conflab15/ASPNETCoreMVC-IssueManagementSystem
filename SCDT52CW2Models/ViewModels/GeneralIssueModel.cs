using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDT52CW2Models.ViewModels
{
    public class GeneralIssueModel
    {
        public GeneralIssue issue { get; set; }

        public List<Update> issueUpdates { get; set; }
    }
}
