using Framework.Core.Entity;
using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Domain.Entities
{
    public class PaymentEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }

        [ForeignKey("CartId")]
        public virtual CartEntity Cart { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; } = default!;
        public decimal Pay { get; set; } = default!;
        public CartStatusEnum Status { get; set; } = CartStatusEnum.Confirmed;
    }
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CartId).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();

            builder.Property(x => x.Total).HasPrecision(18, 2);
            builder.Property(x => x.Pay).HasPrecision(18, 2);

            builder.Property(x => x.Status).IsRequired();
        }
    }
}
