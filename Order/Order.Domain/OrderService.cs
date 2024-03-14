using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Projection;

namespace Order.Domain
{
    public static class OrderService
    {
        public static IServiceCollection AddOrder(this IServiceCollection services)
            => services.Projection(builder =>
                builder
                .AddOn<ProductCreated>(ProductProjection.Handle)
                .AddOn<ProductUpdated>(ProductProjection.Handle)
                .AddOn<ProductPriceVolumeChanged>(ProductProjection.Handle)
                .AddOn<ProductStockSoldChanged>(ProductProjection.Handle)
                .AddOn<ProductStatusChanged>(ProductProjection.Handle)
                .AddOn<UserCreated>(UserProjection.Handle)
        );
    }
}
