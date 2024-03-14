namespace Framework.Core.Events.External
{
    public interface IExternalEventProducer
    {
        Task Publish(IEventEnvelope @event, CancellationToken ct);
    }
}
