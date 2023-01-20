using Error.Areas.Manage.ViewModels;
using Error.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Error.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLogin)
        {
            if (!ModelState.IsValid) return View(); 
            AppUser user = await _userManager.FindByNameAsync(adminLogin.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Username invalid");
                return View();
            }
            var result= await _signInManager.PasswordSignInAsync(user, adminLogin.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Pasword invalid");
                return View();
            }
            return RedirectToAction("index" , "dashboard");
        }


        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();

            }

            return RedirectToAction("login", "account");
        }
    }

    
}
