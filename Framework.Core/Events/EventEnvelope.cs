namespace Framework.Core.Events
{
        public interface IEventEnvelope
        {
            object Data { get; }
        }

        public record EventEnvelope<T>(T Data) : IEventEnvelope where T : notnull
        {
            object IEventEnvelope.Data => Data;
        }
}
