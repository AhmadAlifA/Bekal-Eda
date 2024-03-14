using FluentValidation;
using Framework.Core.Projection;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Dtos;
using Store.Domain.Projections;
using Store.Domain.Validations;

namespace Store.Domain
{
    public static class StoreServices
    {
        public static IServiceCollection AddStore(this IServiceCollection services)
        => services.Projection(builder =>
                builder
                    .AddOn<AttributeCreated>(AttributeProjection.Handle)
                    .AddOn<AttributeUpdated>(AttributeProjection.Handle)
                    .AddOn<AttributeStatusChanged>(AttributeProjection.Handle)
            );

        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
            return services;
        }
    }
}
