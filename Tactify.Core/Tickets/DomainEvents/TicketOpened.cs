using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Tickets.DomainEvents
{
    public sealed class TicketOpened : DomainEvent
    {
        public string BoardId { get; set; }

        public string Description { get; }

        public string CreatedBy { get; }

        public TicketOpened(string ticketId, string boardId, string description, string createdBy) : base(ticketId)
        {
            BoardId = boardId;
            Description = description;
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public TicketOpened(Guid id, string aggregateId, DateTime createdAt, string boardId, string description, string createdBy) : base(id, aggregateId, createdAt)
        {
            BoardId = boardId;
            Description = description;
            CreatedBy = createdBy;
        }
    }
}
