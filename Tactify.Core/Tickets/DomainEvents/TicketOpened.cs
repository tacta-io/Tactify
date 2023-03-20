using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketOpened : DomainEvent
    {
        public string Description { get; }

        public TicketOpened(string aggregateId, string description) : base(aggregateId)
        {
            Description = description;
        }
    }
}
