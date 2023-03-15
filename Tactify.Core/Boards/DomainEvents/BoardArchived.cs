using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardArchived : DomainEvent
    {
        public BoardArchived(string aggregateId) : base(aggregateId)
        {

        }
    }
}
