using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketClosed : DomainEvent
    {
        public string CreatedBy { get; }

        public TicketClosed(string ticketId, string createdBy) : base(ticketId)
        {
            CreatedBy= createdBy;
        }
    }
}
