using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardOpened : DomainEvent
    {
        public string Description { get; }

        public BoardOpened(string aggregateId, string description) : base(aggregateId)
        {
            Description = description;
        }
    }
}
