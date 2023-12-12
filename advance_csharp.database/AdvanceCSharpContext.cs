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
                .UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=sherrin1st_;User Id=sherrin1st_;Password=Taodeptrai1st;Trusted_Connection=False;MultipleActiveResultSets=true");
        }
    }
}
