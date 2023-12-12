using advance_csharp.database.Models;
using Microsoft.EntityFrameworkCore;

namespace advance_csharp.database
{
    public class AdvanceCSharpContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Order>? Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder
                .UseSqlServer("Server=localhost;Database=advance_csharp;User Id=sa;Password=12345;Trusted_Connection=False;MultipleActiveResultSets=true");
        }
    }
}
