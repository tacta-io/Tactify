using Tacta.EventStore.Projector;
using Tactify.Core.ReadModels.TicketReadModels.Repositories;
using Tactify.Core.Tickets.DomainEvents;

namespace Tactify.Core.ReadModels.TicketReadModels.Projections
{
    public sealed class TicketReadModelProjection : Projection
    {
        private readonly ITicketReadModelRepository _ticketReadModelRepository;

        public TicketReadModelProjection(ITicketReadModelRepository ticketReadModelRepository) : base(ticketReadModelRepository)
        {
            _ticketReadModelRepository = ticketReadModelRepository;
        }

        public async Task On(TicketOpened @event)
        {
            var ticket = new TicketReadModel
            {
                Sequence = @event.Sequence,
                Description = @event.Description,
                TicketId = @event.AggregateId,
                BoardId = @event.BoardId,
                IsClosed = false
            };

            await _ticketReadModelRepository.OnTicketOpenedAsync(ticket).ConfigureAwait(false);
        }

        public async Task On(TicketEstimated @event)
        {
            var ticket = new TicketReadModel
            {
                Sequence = @event.Sequence,
                TicketId = @event.AggregateId,
                Estimation = @event.NumberOfDays
            };

            await _ticketReadModelRepository.OnTicketEstimatedAsync(ticket).ConfigureAwait(false);
        }

        public async Task On(TicketMovedToSprint @event)
        {
            var ticket = new TicketReadModel
            {
                Sequence = @event.Sequence,
                TicketId = @event.AggregateId,
                SprintId = @event.SprintId
            };

            await _ticketReadModelRepository.OnTicketMovedToSprintAsync(ticket).ConfigureAwait(false);
        }

        public async Task On(TicketAssigned @event)
        {
            var ticket = new TicketReadModel
            {
                Sequence = @event.Sequence,
                TicketId = @event.AggregateId,
                Assignee = @event.Assignee
            };

            await _ticketReadModelRepository.OnTicketAssignedAsync(ticket).ConfigureAwait(false);
        }

        public async Task On(TicketClosed @event)
        {
            var ticket = new TicketReadModel
            {
                Sequence = @event.Sequence,
                TicketId = @event.AggregateId,
                IsClosed = true
            };

            await _ticketReadModelRepository.OnTicketClosedAsync(ticket).ConfigureAwait(false);
        }
    }
}
