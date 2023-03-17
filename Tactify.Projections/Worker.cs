using Tacta.EventStore.Projector;

namespace Tactify.Projections
{
    public class Worker : BackgroundService
    {
        private const int PullingInterval = 1000;
        private const int BatchSize = 1000;

        private readonly IProjectionProcessor _projectionProcessor;

        public Worker(IProjectionProcessor projectionProcessor)
        {
            _projectionProcessor = projectionProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var processed = await _projectionProcessor.Process(BatchSize);

                if (processed < BatchSize)
                {
                    await Task.Delay(PullingInterval, stoppingToken);
                }
            }
        }
    }
}