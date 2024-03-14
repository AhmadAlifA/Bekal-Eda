using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Framework.Core.Entity;

namespace Store.Domain.Entities
{
    public class CategoryEntity: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
        public virtual ICollection<ProductEntity> Products { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            //Fluent
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Description).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
