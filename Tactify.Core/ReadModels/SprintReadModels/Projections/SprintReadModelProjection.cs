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
                Status = SprintStatus.Created.ToString()
            };

            await _sprintReadModelRepository.SaveSprintReadModelAsync(sprint).ConfigureAwait(false);
        }

        public async Task On(SprintStarted @event)
        {
            await _sprintReadModelRepository
                .UpdateSprintReadModelStatusAsync(@event.AggregateId, @event.SprintId, SprintStatus.Active.ToString())
                .ConfigureAwait(false);
        }

        public async Task On(SprintEnded @event)
        {
            await _sprintReadModelRepository
               .UpdateSprintReadModelStatusAsync(@event.AggregateId, @event.SprintId, SprintStatus.Ended.ToString())
               .ConfigureAwait(false);
        }
    }
}
