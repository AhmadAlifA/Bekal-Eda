using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Domain.Entities;

namespace Order.Domain
{
    public static class ServiceExtension
    {
        public static ConfigurationManager Configuration { get; set; }
        public static void AddDomainContext(this IServiceCollection services, ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
