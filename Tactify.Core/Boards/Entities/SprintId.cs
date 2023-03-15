using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards.Entities
{
    public sealed class SprintId : EntityId
    {      
        public int SprintNumber { get; private set; }

        public SprintId(int sprintNumber)
        {            
            SprintNumber = sprintNumber;
        }

        public static SprintId Identity(string sprintId)
        {
            var values = sprintId.Split("-");

            return new SprintId(int.Parse(values[1]));
        }

        public override string ToString()
        {
            return $"Sprint-{SprintNumber}";
        }
    }
}
