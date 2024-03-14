using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Store.Domain.Entities
{
    public class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {
            
        }

        public DbSet<AttributeEntity> Attributes { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        public static DbContextOptions<StoreDbContext> OnConfigure()
        {
            var optionBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionBuilder.UseSqlServer(ServiceExtension.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine);
            return optionBuilder.Options;
        }
    }
}
