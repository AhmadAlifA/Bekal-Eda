namespace Framework.Core.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken cancellationToken);
    }
}
