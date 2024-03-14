using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Framework.Core.BackgroundServices
{
    public class KafkaService : BackgroundService
    {
        private readonly ILogger<KafkaService> _logger;
        private readonly Func<CancellationToken, Task> _perform;
        public KafkaService(ILogger<KafkaService> logger, Func<CancellationToken, Task> perform)
        {
            _logger = logger;
            _perform = perform;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        => Task.Run(async () =>
        {
            await Task.Yield();
            _logger.LogInformation("Kafka service stopped");
            await _perform(cancellationToken);
            _logger.LogInformation("Kafka service stopped");
        }, cancellationToken);
    }
}
