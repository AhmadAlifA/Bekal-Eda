using Framework.Core.Enums;

namespace Store.Domain.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
        public List<ProductDto?> Products { get; set; } = default!;
    }

    public class CategoryInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }

    public class CategoryStatusDto
    {
        public Guid Id { get; set; }
        public RecordStatusEnum Status { get; set; }
    }
}
