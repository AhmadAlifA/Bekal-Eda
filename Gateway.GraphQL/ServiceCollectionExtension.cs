using System.Net.Http.Headers;

namespace Gateway.GraphQL
{
    public class HttpClientConfig
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("User", async config =>
            {
                config.BaseAddress = new Uri(configuration["HttpClients:UserService"]);
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            services.AddHttpClient("LookUp", async config =>
            {
                config.BaseAddress = new Uri(configuration["HttpClients:LookupService"]);
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            services.AddHttpClient("Store", async config =>
            {
                config.BaseAddress = new Uri(configuration["HttpClients:StoreService"]);
                config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
            });

            services
                .AddGraphQLServer()
                .AddRemoteSchema("User")
                .AddRemoteSchema("LookUp")
                .AddRemoteSchema("Store");

            return services;
        }

        private static async Task<string> GetToken()
        {
            HttpContextAccessor accessor = new HttpContextAccessor();
            HttpContext context = accessor.HttpContext;
            if (context != null)
            {
                var heather = context.Request.Headers.Where(o => o.Key == "Authorization");
                if (heather.Count() > 0)
                    return heather.FirstOrDefault().Value.ToString().Replace("Bearer ", "");
            }
            return null;
        }

    }
}
