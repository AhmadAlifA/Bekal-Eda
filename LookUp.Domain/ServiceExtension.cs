using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LookUp.Domain.Entities;


namespace LookUp.Domain
{
    public static class ServiceExtension
    {
        public static ConfigurationManager Configuration { get; set; }
        public static void AddDomainContext(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<LookUpDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
