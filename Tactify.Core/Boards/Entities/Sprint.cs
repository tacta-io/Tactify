using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.Entities
{
    public sealed class Sprint : Entity<SprintId>
    {
        public override SprintId Id { get; protected set; } 

        public SprintStatus Status { get; private set; }

        public Sprint(SprintId sprintId)
        {
            Id = sprintId;
            Status = SprintStatus.Created;
        }

        public void StartSprint()
        {
            Status = SprintStatus.Active;
        }

        public void EndSprint()
        {
            Status = SprintStatus.Ended;
        }
    }
}
