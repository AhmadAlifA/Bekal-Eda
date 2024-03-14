using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace LookUp.Domain.Entities
{
    public class LookUpDbContext : DbContext
    {
        public LookUpDbContext(DbContextOptions<LookUpDbContext> options) : base(options)
        {

        }
        public DbSet<AttributeEntity> Attributes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AttributeConfiguration());
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AttributeEntity>()
                .HasData
                (
                    new AttributeEntity()
                    {
                        Id = Guid.NewGuid(),
                        Type = AttributeTypeEnum.Text,
                        Unit = "Unit",
                        Status = RecordStatusEnum.Active
                    }
                );
        }
    }
}
