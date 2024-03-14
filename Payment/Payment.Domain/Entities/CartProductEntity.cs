using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Domain.Entities
{
    public class CartProductEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = default!;
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

            builder.Property(x => x.Quantity).HasPrecision(18, 2);
        }
    }
}
