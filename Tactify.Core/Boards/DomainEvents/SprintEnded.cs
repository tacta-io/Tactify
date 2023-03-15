using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintEnded : DomainEvent
    {      
        public string SprintId { get; }

        public SprintEnded(string aggregateId, string sprintId) : base(aggregateId)
        {
            SprintId = sprintId;
        }
    }
}
