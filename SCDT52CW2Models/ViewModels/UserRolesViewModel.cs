using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDT52CW2Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string EmailAddr { get; set; }

        public IEnumerable<string> RoleTitle { get; set; }
    }
}
