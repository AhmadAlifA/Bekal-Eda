﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Polly;
using System.Collections.Concurrent;
using System.Reflection;

namespace Framework.Core.Events
{
    public interface IEventBus
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }

    public class EventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly AsyncPolicy _retryPolicy;

        private static readonly ConcurrentDictionary<Type, MethodInfo> PublishMethods = new();

        public EventBus(IServiceProvider serviceProvider, AsyncPolicy retryPolicy)
        {
            _serviceProvider = serviceProvider;
            _retryPolicy = retryPolicy;
        }

        private async Task Publish<TEvent>(TEvent @event, CancellationToken ct)
        {
            var eventEnvelope = @event as IEventEnvelope;
            using var scope = _serviceProvider.CreateScope();

            var eventHandlers =
                scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                await _retryPolicy.ExecuteAsync(async token =>
                    await eventHandler.Handle(@event, token), ct
                );
            }
        }

        public async Task Publish(IEventEnvelope eventEnvelope, CancellationToken ct)
        {
            // publish also just event data
            // thanks to that both handlers with envelope and without will be called

            await (Task)GetGenericPublishFor(eventEnvelope.Data)
                .Invoke(this, new[] { eventEnvelope.Data, ct })!;

            await (Task)GetGenericPublishFor(eventEnvelope)
                .Invoke(this, new object[] { eventEnvelope, ct })!;
        }

        private static MethodInfo GetGenericPublishFor(object @event) =>
        PublishMethods.GetOrAdd(@event.GetType(), eventType =>
            typeof(EventBus)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Single(m => m.Name == nameof(Publish) && m.GetGenericArguments().Any())
                .MakeGenericMethod(eventType)
        );
    }

    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, AsyncPolicy? asyncPolicy = null)
        {
            services.AddSingleton(serviceProvider => new EventBus(serviceProvider, asyncPolicy ?? Policy.NoOpAsync()));

            services.TryAddSingleton<IEventBus>(sp => sp.GetRequiredService<EventBus>());

            return services;
        }
    }
}
