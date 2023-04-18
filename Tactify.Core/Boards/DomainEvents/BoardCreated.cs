using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class BoardCreated : DomainEvent
    {
        public string Description { get; }

        public string CreatedBy { get; }

        public BoardCreated(string boardId, string description, string createdBy) : base(boardId)
        {
            Description = description;
            CreatedBy = createdBy;
        }
    }
}
