using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Order.Domain.Entities
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartProductEntity> CartProducts { get; set; }
        public DbSet<UserEntity> Customers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public static DbContextOptions<OrderDbContext> OnConfigure()
        {
            var optionBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine);
            return optionBuilder.Options;
        }
    }



}

