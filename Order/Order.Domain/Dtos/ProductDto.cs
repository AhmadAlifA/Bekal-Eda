using Framework.Core.Enums;

namespace Order.Domain.Dtos
{
    public class ProductDto
    { 
            public Guid Id { get; set; }
            public string Sku { get; set; } = default!;
            public string Name { get; set; } = default!;
            public decimal Price { get; set; } = default!;
            public decimal Volume { get; set; } = default!;
            public int Sold { get; set; } = default!;
            public int Stock { get; set; } = default!;
            public RecordStatusEnum Status { get; set; } = default!;
    }
}
