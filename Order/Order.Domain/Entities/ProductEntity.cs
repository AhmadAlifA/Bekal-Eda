using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Volume { get; set; } = default!;
        public int Sold { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
    }

    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            //Fluent
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Sku).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.Volume).HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.Sold).HasDefaultValue(0);
            builder.Property(x => x.Stock).HasDefaultValue(0);
            builder.Property(x => x.Status).HasDefaultValue(RecordStatusEnum.InActive);
        }
    }
}
