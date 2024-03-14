using Confluent.Kafka;
using Framework.Core.Events;
using Framework.Core.Reflection;
using Framework.Core.Serialization.Newtonsoft;

namespace Framework.Kafka.Events
{
    public static class EventEnvelopeExtensions
    {
        public static IEventEnvelope? ToEventEnvelope(this ConsumeResult<string, string> message)
        {
            var eventType = TypeProvider.GetTypeFromAnyReferencingAssembly(message.Message.Key);

            if (eventType == null)
                return null;

            var eventEnvelopeType = typeof(EventEnvelope<>).MakeGenericType(eventType);

            // deserialize event
            return message.Message.Value.FromJson(eventEnvelopeType) as IEventEnvelope;
        }
    }
}

