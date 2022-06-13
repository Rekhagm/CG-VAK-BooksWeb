using Microsoft.EntityFrameworkCore;
using CGVakBooks.Models;

namespace CGVakBooks.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<products> products { get; set; }

        public DbSet<CoverType> covertype { get; set; }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
