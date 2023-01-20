using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Error.Models
{

    public class PustokContext : IdentityDbContext
    {
        public PustokContext(DbContextOptions<PustokContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookImage> bookImages { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<BasketItem> basketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        


      
    }
}
