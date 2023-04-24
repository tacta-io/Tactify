using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketMovedToSprint : DomainEvent
    {
        public string SprintId { get; }

        public string CreatedBy { get; }

        public TicketMovedToSprint(string ticketId, string sprintId, string createdBy) : base(ticketId)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public TicketMovedToSprint(Guid id, string aggregateId, DateTime createdAt, string sprintId, string createdBy) : base(id, aggregateId, createdAt)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }
    }
}
