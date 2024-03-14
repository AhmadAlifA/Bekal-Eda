using Framework.Core.Enums;
using Framework.Core.Events;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Payment.Domain.Entities;
using Payment.Domain.Services;

namespace Payment.Domain.Projection
{
    public record CartCreated(
    Guid Id,
    Guid CustomerId,
    CartStatusEnum Status
    );
    public record CartStatusChanged(
        Guid Id,
        Guid CustomerId,
        List<CartProductItem> CartProducts,
        CartStatusEnum Status
    );
    public class CartProductItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class CartProjection
    {
        private readonly IPaymentService _service;

        public CartProjection(IPaymentService service)
        {
            _service = service;
        }
        public static bool Handle(EventEnvelope<CartCreated> eventEnvelope)
        {
            var (id, customerId, status) = eventEnvelope.Data;
            using (var context = new PaymentDbContext(PaymentDbContext.OnConfigure()))
            {
                CartEntity entity = new CartEntity()
                {
                    Id = (Guid)id,
                    CustomerId = customerId,
                    Status = status
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
        public static bool Handle(EventEnvelope<CartStatusChanged> eventEnvelope)
        {
            var (id, customerId, cartProducts, status) = eventEnvelope.Data;
            using (var context = new PaymentDbContext(PaymentDbContext.OnConfigure()))
            {
                CartEntity entity = context.Carts.Where(o => o.Id == id).FirstOrDefault();
                if (entity == null)
                {
                    CartEntity newEntity = new CartEntity()
                    {
                        Id = (Guid)id,
                        CustomerId = customerId,
                        Status = status
                    };
                    context.Carts.Add(newEntity);
                    context.SaveChanges();
                }
                else
                {
                    foreach (var item in cartProducts)
                    {
                        CartProductEntity cartProd = new CartProductEntity()
                        {
                            Id = (Guid)item.Id,
                            CartId = id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };
                        context.CartProducts.Add(cartProd);
                    }

                    PaymentEntity paymentEntity = new PaymentEntity()
                    {
                        Id= Guid.NewGuid(),
                        CartId = entity.Id,
                        CustomerId = entity.CustomerId,
                        Total = entity.Total,
                        Status = CartStatusEnum.Confirmed
                    };
                    context.Payments.Add(paymentEntity);
                    context.SaveChanges();
                }
            }
            return true;
        }
    }
}
