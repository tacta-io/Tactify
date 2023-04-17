using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintStarted : DomainEvent
    {       
        public string SprintId { get; }

        public string CreatedBy { get; }

        public SprintStarted(string aggregateId, string sprintId, string createdBy) : base(aggregateId)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }
    }
}
