using Framework.Core.BackgroundServices;
using Framework.Core.Events.External;
using Framework.Kafka.Consumers;
using Framework.Kafka.Producers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Framework.Kafka
{
    public static class Config
    {
        public static IServiceCollection AddKafkaProducer(this IServiceCollection services)
        {
            //using TryAdd to support mocking, without that it won't be possible to override in tests
            services.TryAddSingleton<IExternalEventProducer, KafkaProducer>();
            return services;
        }

        public static IServiceCollection AddKafkaConsumer(this IServiceCollection services)
        {
            //using TryAdd to support mocking, without that it won't be possible to override in tests
            services.TryAddSingleton<IExternalEventConsumer, KafkaConsumer>();

            return services.AddHostedService(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<KafkaService>>();
                var consumer = serviceProvider.GetRequiredService<IExternalEventConsumer>();

                return new KafkaService(logger, consumer.StartAsync);
            });
        }
    }
}

