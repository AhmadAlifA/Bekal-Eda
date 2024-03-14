namespace Framework.Core.Events.External
{
    public class NulloExternalEventProducer
    {
        public Task Publish(IEventEnvelope @event, CancellationToken ct)
        {
            return Task.CompletedTask;
        }
    }
}
