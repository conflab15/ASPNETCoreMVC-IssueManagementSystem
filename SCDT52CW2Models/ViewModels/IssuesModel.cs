using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDT52CW2Models.ViewModels
{
    public class IssuesModel
    {
        public IEnumerable<Issue> generalIssue { get; set; } //List General Issues

        public IEnumerable<Issue> technicalIssue { get; set; } //List Technical Issues
    }
}
