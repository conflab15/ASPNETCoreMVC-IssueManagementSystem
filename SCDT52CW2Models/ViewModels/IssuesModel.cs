using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDT52CW2Models.ViewModels
{
    public class IssuesModel
    {
        public IEnumerable<GeneralIssue> generalIssue { get; set; }

        public IEnumerable<TechnicalIssue> technicalIssue { get; set; }
    }
}
