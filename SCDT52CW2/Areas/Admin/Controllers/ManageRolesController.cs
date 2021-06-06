using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SCDT52CW2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

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

            var role = new IdentityRole();

            role.Name = roleName;

            await _roleManager.CreateAsync(role);
            
            return RedirectToAction("Index");
        }
    }
}
