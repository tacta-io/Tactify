using System.ComponentModel;

namespace Tactify.Core.ReadModels
{
    public abstract class ReadModel
    {
        [Description("ignore")] public int Id { get; }

        public DateTime UpdatedAt { get; }

        public int Sequence { get; }

        public Guid EventId { get; }

        public ReadModel(DateTime updatedAt, int sequence, Guid eventId)
        {
            UpdatedAt = updatedAt;
            Sequence = sequence;
            EventId = eventId;
        }
    }
}
