using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketAssigned : DomainEvent
    {
        public string Assignee { get; }

        public string CreatedBy { get; }

        public TicketAssigned(string ticketId, string assignee, string createdBy) : base(ticketId)
        {
            Assignee = assignee;
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public TicketAssigned(Guid id, string aggregateId, DateTime createdAt, string assignee, string createdBy) : base(id, aggregateId, createdAt)
        {
            Assignee = assignee;
            CreatedBy = createdBy;
        }
    }
}
