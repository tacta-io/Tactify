using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardArchived : DomainEvent
    {
        public string CreatedBy { get; }

        public BoardArchived(string aggregateId, string createdBy) : base(aggregateId)
        {
            CreatedBy = createdBy;
        }
    }
}
