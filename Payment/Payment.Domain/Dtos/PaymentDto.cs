using Framework.Core.Enums;

namespace Payment.Domain.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; } = default!;
        public decimal Pay { get; set; } = default!;
        public CartStatusEnum Status { get; set; } = default!;
        public CartDto Cart { get; set; }
    }

    public class PaymentCreateDto
    {
    }
    public class PaymentUpdateDto
    {
        public Guid Id { get; set; }
        public decimal Pay { get; set; } = default!;
        public CartStatusEnum Status { get; set; } = CartStatusEnum.Paid;

    }

    public class PaymentCartDto
    {

    }

    public class PaymentStatusDto
    {
        public Guid Id { get; set; }
        public CartStatusEnum Status { get; set; } = CartStatusEnum.Canceled;
    }
}
