using Framework.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Dtos
{
    public class CartProductDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string Sku { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.Active;
        public CartDto Cart { get; set; }
        public ProductDto Product { get; set; }
    }
    public class CartProductCreateDto
    {
        public Guid? Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = default!;
        public string? Sku { get; set; } = default!;
        public string? Name { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
    }
    public class CartProductUpdateDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; } = default!;
    }
    public class CartProductCartDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
    }
    public class CartProductProductDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
    }
}
