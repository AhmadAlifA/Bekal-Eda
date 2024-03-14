using Framework.Core.Entity;
using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LookUp.Domain.Entities
{
    public class AttributeEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
    }
    public class AttributeConfiguration : IEntityTypeConfiguration<AttributeEntity>
    {
        public void Configure(EntityTypeBuilder<AttributeEntity> builder)
        {
            //Fluent
            builder.ToTable("Attributes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Unit).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Unit).IsUnique();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
