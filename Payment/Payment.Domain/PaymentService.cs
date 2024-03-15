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
                    .AddOn<CartStatusChanged>(CartProjection.Handle)
            );
    }
}
