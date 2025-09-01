using Microsoft.EntityFrameworkCore;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketItem>();

            modelBuilder.Entity<ProcessedBasketItem>();

            modelBuilder.Entity<Discount>();
        }

        public DbSet<BasketItem> BasketItem { get; set; }

        public DbSet<Discount> BasketDiscounts { get; set; }

        public DbSet<ProcessedBasketItem> ProcessedBasketItem { get; set; }
    }
}
