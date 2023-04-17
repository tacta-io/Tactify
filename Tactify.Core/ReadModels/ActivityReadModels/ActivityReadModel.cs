namespace Tactify.Core.ReadModels.ActivityReadModels
{
    public sealed class ActivityReadModel : ReadModel
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
