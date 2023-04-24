using Newtonsoft.Json;
using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintEnded : DomainEvent
    {      
        public string SprintId { get; }

        public string CreatedBy { get; }

        public SprintEnded(string boardId, string sprintId, string createdBy) : base(boardId)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }

        [JsonConstructor]
        public SprintEnded(Guid id, string aggregateId, DateTime createdAt, string sprintId, string createdBy) : base(id, aggregateId, createdAt)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }
    }
}
