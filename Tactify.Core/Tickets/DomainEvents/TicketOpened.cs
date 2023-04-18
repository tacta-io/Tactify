using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketOpened : DomainEvent
    {
        public string Description { get; }

        public string CreatedBy { get; }

        public TicketOpened(string ticketId, string description, string createdBy) : base(ticketId)
        {
            Description = description;
            CreatedBy = createdBy;
        }
    }
}
