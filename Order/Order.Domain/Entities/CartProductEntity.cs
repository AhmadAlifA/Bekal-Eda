using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entities
{
    public class CartProductEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual CartEntity Cart { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductEntity Product { get; set; }
        public int Quantity { get; set; } = default!;
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }

    public class CartProductConfiguration : IEntityTypeConfiguration<CartProductEntity>
    {
        public void Configure(EntityTypeBuilder<CartProductEntity> builder)
        {
            //Fluent
            builder.ToTable("CartProducts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CartId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();

            builder.Property(x => x.Sku).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Quantity).HasPrecision(18, 2);
            builder.Property(x => x.Price).HasPrecision(18, 2);
        }
    }
}
