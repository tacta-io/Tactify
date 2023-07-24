using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.Entities
{
    public sealed class Sprint : Entity<SprintId>
    {
        public override SprintId Id { get; protected set; } 

        public SprintStatus Status { get; private set; }

        public bool IsEnded => Status == SprintStatus.Ended;

        public Sprint(SprintId sprintId)
        {
            Id = sprintId;
            Status = SprintStatus.Created;
        }

        public void SprintStarted() => Status = SprintStatus.Active;

        public void SprintEnded() => Status = SprintStatus.Ended;
    }
}
