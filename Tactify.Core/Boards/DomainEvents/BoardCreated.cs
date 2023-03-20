using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardCreated : DomainEvent
    {
        public string Description { get; }

        public BoardCreated(string aggregateId, string description) : base(aggregateId)
        {
            Description = description;
        }
    }
}
