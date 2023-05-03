namespace Tactify.Core.ReadModels.ActivityReadModels
{
    public sealed class ActivityReadModel
    {
        public int Sequence { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
