using Microsoft.EntityFrameworkCore;
using MobilePhoneStore.Models;

namespace MobilePhoneStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MobilePhone> MobilePhones { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MobilePhone>()
            .HasOne(m => m.Brand)
            .WithMany(b => b.MobilePhones)
            .HasForeignKey(m => m.BrandId);
    }



    }
}
