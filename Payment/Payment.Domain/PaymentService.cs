using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Projection;

namespace Payment.Domain
{
    public static class PaymentServices
    {
        public static IServiceCollection AddPayment(this IServiceCollection services)
            => services.Projection(builder =>
                builder
                    .AddOn<CartCreated>(CartProjection.Handle)
                    .AddOn<CartUpdated>(CartProjection.Handle)
                    .AddOn<CartStatusChanged>(CartProjection.Handle)
                    .AddOn<CartProductCreated>(CartProductProjection.Handle)
                    .AddOn<CartProductUpdated>(CartProductProjection.Handle)
                    .AddOn<CartProductCartChanged>(CartProductProjection.Handle)
                    .AddOn<CartProductProductChanged>(CartProductProjection.Handle)
            );
    }
}
