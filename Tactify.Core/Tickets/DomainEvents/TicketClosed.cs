using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketClosed : DomainEvent
    {
        public string CreatedBy { get; }

        public TicketClosed(string ticketId, string createdBy) : base(ticketId)
        {
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public TicketClosed(Guid id, string aggregateId, DateTime createdAt, string createdBy) : base(id, aggregateId, createdAt)
        {
            CreatedBy = createdBy;
        }
    }
}
