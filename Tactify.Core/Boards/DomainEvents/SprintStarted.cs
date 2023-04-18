using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.DomainEvents
{
    public sealed class SprintStarted : DomainEvent
    {       
        public string SprintId { get; }

        public string CreatedBy { get; }

        public SprintStarted(string boardId, string sprintId, string createdBy) : base(boardId)
        {
            SprintId = sprintId;
            CreatedBy = createdBy;
        }
    }
}
