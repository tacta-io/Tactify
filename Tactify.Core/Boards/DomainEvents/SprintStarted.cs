using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintStarted : DomainEvent
    {       
        public string SprintId { get; }

        public SprintStarted(string aggregateId, string sprintId) : base(aggregateId)
        {
            SprintId = sprintId;
        }
    }
}
