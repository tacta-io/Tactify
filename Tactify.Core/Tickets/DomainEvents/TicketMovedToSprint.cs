using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketMovedToSprint : DomainEvent
    {
        public string SprintId { get; }

        public TicketMovedToSprint(string aggregateId, string sprintId) : base(aggregateId)
        {
            SprintId = sprintId;
        }
    }
}
