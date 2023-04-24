using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardCreated : DomainEvent
    {
        public string Description { get; }

        public string CreatedBy { get; }

        public BoardCreated(string boardId, string description, string createdBy) : base(boardId)
        {
            Description = description;
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public BoardCreated(Guid id, string aggregateId, DateTime createdAt, string description, string createdBy) : base(id, aggregateId, createdAt)
        {
            Description = description;
            CreatedBy = createdBy;
        }
    }
}
