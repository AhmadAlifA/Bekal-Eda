﻿using Framework.Core.Entity;
using Framework.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryEntity Category { get; set; }
        public Guid AttributeId { get; set; }

        [ForeignKey("AttributeId")]
        public virtual AttributeEntity Attribute { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
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
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.AttributeId).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Sku).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Price).HasPrecision(18,2);
            builder.Property(x => x.Volume).HasPrecision(18, 2);
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
