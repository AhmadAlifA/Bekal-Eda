using Framework.Core.Enums;

namespace Payment.Domain.EventEnvelopes
{
    public record PaymentCreated
    (
        Guid Id,
        Guid CartId,
        Guid CustomerId,
        decimal Total,
        decimal Pay,
        CartStatusEnum Status
    )
    { 
        public static PaymentCreated Create(Guid id,
            Guid cartId,
            Guid customerId,
            decimal total,
            decimal pay,
            CartStatusEnum status
                ) => new(id, cartId, customerId, total, pay, status );
    }

    public record PaymentUpdated
    ( Guid Id, decimal Pay )
    {
        public static PaymentUpdated Create(Guid id,
            decimal pay
                ) => new(id, pay);
    }

    public record PaymentCartChanged
    ( Guid Id, Guid CartId )
    {
        public static PaymentCartChanged Create(Guid id, Guid cartId
                ) => new(id, cartId);
    }

    public record PaymentStatusChanged
    ( Guid Id, CartStatusEnum Status )
    {
        public static PaymentStatusChanged Create(Guid id, CartStatusEnum status
                ) => new(id, status);
    }
}
