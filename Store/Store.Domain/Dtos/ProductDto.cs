using Framework.Core.Enums;

namespace Store.Domain.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid AttributeId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Volume { get; set; } = default!;
        public int Sold { get; set; } = default!;
        public int Stock { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
        public CategoryDto? Category { get; set; }
        public AttributeDto? Attribute { get; set; }
    }
    public class ProductCreateDto
    {
        public Guid CategoryId { get; set; }
        public Guid AttributeId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class ProductCategoryDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class ProductAttributeDto
    {
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }
    }

    public class ProductPriceVolumeDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
    }

    public class ProductStockSoldDto
    {
        public Guid Id { get; set; }
        public int Stock { get; set; }
        public int Sold { get; set; }
    }

    public class ProductStatusDto
    {
        public Guid Id { get; set; }
        RecordStatusEnum Status { get; set; }
    }
}
