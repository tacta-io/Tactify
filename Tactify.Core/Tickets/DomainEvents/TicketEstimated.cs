using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketEstimated : DomainEvent
    {
        public int NumberOfDays { get; }

        public string CreatedBy { get; }


        public TicketEstimated(string ticketId, int numberOfDays, string createdBy) : base(ticketId)
        {
            NumberOfDays = numberOfDays;
            CreatedBy = createdBy;
        }
    }
}
