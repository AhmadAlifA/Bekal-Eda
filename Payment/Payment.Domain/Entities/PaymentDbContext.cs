using HotChocolate.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Payment.Domain.Entities
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {

        }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<CartProductEntity> CartProducts { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartProductConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
        public static DbContextOptions<PaymentDbContext> OnConfigure()
        {
            var optionBuilder = new DbContextOptionsBuilder<PaymentDbContext>();
            optionBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine);
            return optionBuilder.Options;
        }
    }
}
