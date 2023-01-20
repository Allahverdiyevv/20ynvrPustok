using Error.Models;
using Error.ViewModels.Member;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Error.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly PustokContext _pustokContext;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager , PustokContext pustokContext , SignInManager<AppUser>signInManager)
		{
			_userManager = userManager;
			_pustokContext = pustokContext;
			_signInManager = signInManager;
		}


		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
		{
			AppUser member =await _userManager.FindByNameAsync(memberRegisterVM.Username);

			if(!ModelState.IsValid) return View();

			if( member != null)
			{
				ModelState.AddModelError("Username", "User Name Has Tacen!");
				return View();
			}

			//member = await _pustokContext.Users.FirstOrDefaultAsync(x=>x.NormalizedEmail == memberRegisterVM.Email.ToUpper());

			member = await _userManager.FindByEmailAsync(memberRegisterVM.Email);
			if (member != null)
			{
				ModelState.AddModelError("Email", "Email Has Tacen!");
				return View();

			}

			member = new AppUser
			{
				FulName = memberRegisterVM.Fullname,
				UserName = memberRegisterVM.Username,
				Email = memberRegisterVM.Email,
			};

			var rezult=  await _userManager.CreateAsync(member, memberRegisterVM.Pasword);
			if (!rezult.Succeeded)
			{
				foreach (var eror in rezult.Errors)
				{
					ModelState.AddModelError("", eror.Description);
					return View();
				}
			}
			

			var RoleResult = await _userManager.AddToRoleAsync(member, "Member");
			if (!RoleResult.Succeeded)
			{
				foreach (var er in RoleResult.Errors)
				{

					ModelState.AddModelError("", er.Description);
					return View();
				}
			}

			await _signInManager.SignInAsync(member, isPersistent: false);

			return RedirectToAction("index", "Home");
		}


		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(MemberLoginViewModel memberLoginViewModel)
		{
			if (!ModelState.IsValid) return View();

			AppUser appUser = await _userManager.FindByNameAsync(memberLoginViewModel.UserName);

			if(appUser == null)
			{
				ModelState.AddModelError("", "User Name ıs pasword incorrect!");
				return View();

			}
			var result = await _signInManager.PasswordSignInAsync(appUser, memberLoginViewModel.Pasword, false, false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "User Name ıs pasword incorrect!");
				return View();
			}

			return RedirectToAction("index" , "home");
		}
		public async Task<IActionResult>  Logout()
		{

			await _signInManager.SignOutAsync();

			return RedirectToAction("login", "account");
		}
	}
}
