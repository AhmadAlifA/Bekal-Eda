using Framework.Core.Enums;

namespace LookUp.Domain.Dtos
{
    public class AttributeDto
    {
        public Guid? Id { get; set; }
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        public string Unit { get; set; } = default!;
        public RecordStatusEnum Status { get; set; } = RecordStatusEnum.InActive;
        //public DateTime? Modified { get; set; } = DateTime.Now;
    }

    public class AttributeExceptStatusDto
    {
        public Guid Id { get; set; }
        public AttributeTypeEnum Type { get; set; }
        public string Unit { get; set; } = default!;
    }

    public class AttributeStatusDto
    {
        public Guid Id { get; set; }
        public RecordStatusEnum Status { get; set; }
    }
}
