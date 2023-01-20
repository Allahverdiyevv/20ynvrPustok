using Error.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Error.Services
{
	public class MainLayoutServices
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;


		public MainLayoutServices(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
		{
			_userManager=userManager;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<AppUser> GetUser()
		{
			AppUser appUser = null;

			if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
				appUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
			};
			if (appUser != null) return appUser;

			return null;
			
		}
	}
}
