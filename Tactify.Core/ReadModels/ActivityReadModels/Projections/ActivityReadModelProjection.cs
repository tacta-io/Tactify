﻿using Tacta.EventStore.Projector;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.ReadModels.ActivityReadModels.Repositories;

namespace Tactify.Core.ReadModels.ActivityReadModels.Projections
{
    public sealed class ActivityReadModelProjection : Projection
    {

        private readonly IActivityReadModelRepository _activityReadModelRepository;

        public ActivityReadModelProjection(IActivityReadModelRepository activityReadModelRepository) : base(activityReadModelRepository)
        {
            _activityReadModelRepository = activityReadModelRepository;
        }

        public async Task On(BoardCreated @event)
        {
            var activity = new ActivityReadModel
            {
                Sequence = @event.Sequence,
                CreatedAt = @event.CreatedAt,
                CreatedBy = @event.CreatedBy,
                Name = @event.GetType().Name,
                Description = @event.AggregateId
            };

            await _activityReadModelRepository.SaveActivityReadModelAsync(activity).ConfigureAwait(false);
        }

        public async Task On(SprintCreated @event)
        {
            var activity = new ActivityReadModel
            {
                Sequence = @event.Sequence,
                CreatedAt = @event.CreatedAt,
                CreatedBy = @event.CreatedBy,
                Name = @event.GetType().Name,
                Description = $"{@event.AggregateId} {@event.SprintId}"
            };

            await _activityReadModelRepository.SaveActivityReadModelAsync(activity).ConfigureAwait(false);
        }

        public async Task On(SprintStarted @event)
        {
            var activity = new ActivityReadModel
            {
                Sequence = @event.Sequence,
                CreatedAt = @event.CreatedAt,
                CreatedBy = @event.CreatedBy,
                Name = @event.GetType().Name,
                Description = $"{@event.AggregateId} {@event.SprintId}"
            };

            await _activityReadModelRepository.SaveActivityReadModelAsync(activity).ConfigureAwait(false);
        }

        public async Task On(SprintEnded @event)
        {
            var activity = new ActivityReadModel
            {
                Sequence = @event.Sequence,
                CreatedAt = @event.CreatedAt,
                CreatedBy = @event.CreatedBy,
                Name = @event.GetType().Name,
                Description = $"{@event.AggregateId} {@event.SprintId}"
            };
            await _activityReadModelRepository.SaveActivityReadModelAsync(activity).ConfigureAwait(false);
        }

        public async Task On(BoardArchived @event)
        {
            var activity = new ActivityReadModel
            {
                Sequence = @event.Sequence,
                CreatedAt = @event.CreatedAt,
                CreatedBy = @event.CreatedBy,
                Name = @event.GetType().Name,
                Description = @event.AggregateId
            };

            await _activityReadModelRepository.SaveActivityReadModelAsync(activity).ConfigureAwait(false);
        }
    }
}