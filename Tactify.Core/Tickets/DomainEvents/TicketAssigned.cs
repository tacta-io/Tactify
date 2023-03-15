using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketAssigned : DomainEvent
    {
        public string Assignee { get; }

        public TicketAssigned(string aggregateId, string assignee) : base(aggregateId)
        {
            Assignee = assignee;
        }
    }
}
