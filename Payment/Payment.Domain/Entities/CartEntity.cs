using Framework.Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Core.Enums;

namespace Payment.Domain.Entities
{
    public class CartEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatusEnum Status { get; set; }
    }

    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();

            builder.Property(x => x.Status).IsRequired();
        }
    }
}
