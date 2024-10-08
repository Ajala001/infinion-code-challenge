using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.infrastructure.DataContext
{
    public class User_ProductDb : DbContext
    {
        public User_ProductDb(DbContextOptions<User_ProductDb> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<User>().HasKey(u => u.Id);

        }
    }
}
