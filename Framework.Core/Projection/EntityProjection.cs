using Framework.Core.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Core.Projection
{
    public static class EntityProjection
    {
        public static IServiceCollection Projection(this IServiceCollection services, Action<ProjectionBuilder> builder)
        {
            builder(new ProjectionBuilder(services));
            return services;
        }
    }

    public class ProjectionBuilder
    {
        public readonly IServiceCollection _service;
        public ProjectionBuilder(IServiceCollection service)
        {
            _service = service;
        }
        public ProjectionBuilder AddOn<TEvent>(Func<EventEnvelope<TEvent>, bool> onHandle) where TEvent : notnull
        {
            _service.AddTransient<IEventHandler<EventEnvelope<TEvent>>>(sp =>
            {
                return new AddProjection<TEvent>(onHandle);
            });
            return this;
        }
    }

    public class AddProjection<TEvent> : IEventHandler<EventEnvelope<TEvent>> where TEvent : notnull
    {
        private readonly Func<EventEnvelope<TEvent>, bool> _onCreate;
        public AddProjection(Func<EventEnvelope<TEvent>, bool> onCreate)
        {
            _onCreate = onCreate;
        }
        public async Task Handle(EventEnvelope<TEvent> eventEnvelope, CancellationToken cancellationToken)
        {
            var view = _onCreate(eventEnvelope);
        }
    }
}
