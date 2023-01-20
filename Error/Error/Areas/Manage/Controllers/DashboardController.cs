using Error.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Error.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin")]
    public class DashboardController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager ,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        FulName = "Polad Allahverdiyev",
        //        UserName = "SuperAdmin"

        //    };
        //    var result = await _userManager.CreateAsync(user, "Admin123");

        //    return Ok(result);
        //}

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok("Create");


        }

        public async Task<IActionResult> AddRole()
        {
            AppUser user = await _userManager.FindByNameAsync("SuperAdmin");

            await _userManager.AddToRoleAsync(user, "SuperAdmin");
            return Ok("");
        }
    }
}
