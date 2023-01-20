using Error.Models;
using Microsoft.AspNetCore.Identity;

namespace Error.Areas.Manage.Services
{
    public class LayoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

        }
        public async Task<AppUser> GetUser()
        {
            AppUser appUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

                

             return appUser;
            
        }
    }
}
