using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketClosed : DomainEvent
    {
        public TicketClosed(string aggregateId) : base(aggregateId)
        {

        }
    }
}
