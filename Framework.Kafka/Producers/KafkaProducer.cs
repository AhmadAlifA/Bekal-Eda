using Confluent.Kafka;
using Framework.Core.Events;
using Framework.Core.Events.External;
using Framework.Core.Serialization.Newtonsoft;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Framework.Kafka.Producers
{
    public class KafkaProducer: IExternalEventProducer
    {
        private readonly KafkaProducerConfig _config;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(IConfiguration configuration, ILogger<KafkaProducer> logger)
        {
            _config = configuration.GetKafkaProducerConfig();
            _logger = logger;
        }

        public async Task Publish(IEventEnvelope @event, CancellationToken ct)
        {
            try
            {
                using var p = new ProducerBuilder<string, string>(_config.ProducerConfig).Build();

                await p.ProduceAsync(_config.Topic,
                    new Message<string, string>
                    {
                        Key = @event.Data.GetType().Name,
                        Value = @event.ToJson(),
                    }, ct).ConfigureAwait(false);
            }
            catch (Exception e) 
            {
                _logger.LogError("Error producing Kafka message: {Message} {StackTrace}", e.Message, e.StackTrace);
                throw;
            }
        }
    }
}
