using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketCreated : DomainEvent
    {
        public string Description { get; }

        public TicketCreated(string aggregateId, string description) : base(aggregateId)
        {
            Description = description;
        }
    }
}
