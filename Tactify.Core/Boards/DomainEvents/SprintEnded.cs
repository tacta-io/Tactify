using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintEnded : DomainEvent
    {      
        public string SprintId { get; }

        public string CreatedBy { get; }

        public SprintEnded(string aggregateId, string sprintId, string createdBy) : base(aggregateId)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }
    }
}
