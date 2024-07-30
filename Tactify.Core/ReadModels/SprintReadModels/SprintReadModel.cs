namespace Tactify.Core.ReadModels.SprintReadModels
{
    public sealed class SprintReadModel
    {
        public long Sequence { get; set; }

        public string BoardId { get; set; }

        public string SprintId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }
    }
}
