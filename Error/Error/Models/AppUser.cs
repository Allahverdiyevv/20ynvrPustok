using Microsoft.AspNetCore.Identity;

namespace Error.Models
{
    public class AppUser :IdentityUser
    {
        public string FulName { get; set; }
        public bool IsAdmin { get; set; }
        public List<BasketItem> basketItems { get; set; }   
    }
}
