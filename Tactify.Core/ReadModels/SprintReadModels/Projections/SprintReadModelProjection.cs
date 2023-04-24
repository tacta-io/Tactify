using Tacta.EventStore.Projector;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.Boards.Entities;
using Tactify.Core.ReadModels.SprintReadModels.Repositories;

namespace Tactify.Core.ReadModels.SprintReadModels.Projections
{
    public sealed class SprintReadModelProjection : Projection
    {
        private readonly ISprintReadModelRepository _sprintReadModelRepository;

        public SprintReadModelProjection(ISprintReadModelRepository sprintReadModelRepository) : base(sprintReadModelRepository)
        {
            _sprintReadModelRepository = sprintReadModelRepository;
        }

        public async Task On(SprintCreated @event)
        {
            var sprint = new SprintReadModel
            {
                Sequence = @event.Sequence,
                BoardId = @event.AggregateId,
                SprintId = @event.SprintId,
                CreatedAt = @event.CreatedAt,
                Status = SprintStatus.Created.ToString()
            };

            await _sprintReadModelRepository.OnSprintCreatedAsync(sprint).ConfigureAwait(false);
        }

        public async Task On(SprintStarted @event)
        {
            var sprint = new SprintReadModel
            {
                Sequence = @event.Sequence,
                BoardId = @event.AggregateId,
                SprintId = @event.SprintId,
                StartedAt = @event.CreatedAt,
                Status = SprintStatus.Created.ToString()
            };

            await _sprintReadModelRepository.OnSprintStartedAsync(sprint).ConfigureAwait(false);
        }

        public async Task On(SprintEnded @event)
        {
            var sprint = new SprintReadModel
            {
                Sequence = @event.Sequence,
                BoardId = @event.AggregateId,
                SprintId = @event.SprintId,
                EndedAt = @event.CreatedAt,
                Status = SprintStatus.Ended.ToString()
            };

            await _sprintReadModelRepository.OnSprintEndedAsync(sprint).ConfigureAwait(false);
        }
    }
}
