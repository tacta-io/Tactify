using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardArchived : DomainEvent
    {
        public string CreatedBy { get; }

        public BoardArchived(string boardId, string createdBy) : base(boardId)
        {
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public BoardArchived(Guid id, string aggregateId, DateTime createdAt, string createdBy) : base(id, aggregateId, createdAt)
        {
            CreatedBy = createdBy;
        }
    }
}
