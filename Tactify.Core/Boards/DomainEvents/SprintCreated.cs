using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintCreated : DomainEvent
    {
        public string SprintId { get; }

        public SprintCreated(string aggregateId, string sprintId) : base(aggregateId)
        {
            SprintId = sprintId;
        }
    }
}
