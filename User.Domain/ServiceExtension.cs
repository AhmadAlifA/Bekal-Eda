﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Entities;

namespace User.Domain
{
    public static class ServiceExtension
    {
        public static ConfigurationManager Configuration { get; set; }
        public static void AddDomainContext(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            Configuration = configuration;
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
