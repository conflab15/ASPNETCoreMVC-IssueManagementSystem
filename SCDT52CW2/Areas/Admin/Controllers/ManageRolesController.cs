using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SCDT52CW2.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ManageRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        //Manage Roles Controller allows Administrators to create new roles which can be assigned to users of the system within the UserRoles controller and view...

        public ManageRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            //Taking a roleName from the Form and view, and then creating a new IdentityRole and assigning the given name to the IdentityRole

            var role = new IdentityRole();

            role.Name = roleName;

            await _roleManager.CreateAsync(role);
            
            return RedirectToAction("Index");
        }
    }
}
