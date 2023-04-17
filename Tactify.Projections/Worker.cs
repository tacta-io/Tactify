using Tacta.EventStore.Projector;

namespace Tactify.Projections
{
    public class Worker : BackgroundService
    {
        private const int PullingInterval = 500;
        private const int BatchSize = 500;
        private const bool ProcessParallel = true;

        private readonly IProjectionProcessor _projectionProcessor;

        public Worker(IProjectionProcessor projectionProcessor)
        {
            _projectionProcessor = projectionProcessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var processed = await _projectionProcessor.Process(BatchSize, ProcessParallel).ConfigureAwait(false);

                if (processed < BatchSize)
                {
                    await Task.Delay(PullingInterval, stoppingToken);
                }
            }
        }
    }
}