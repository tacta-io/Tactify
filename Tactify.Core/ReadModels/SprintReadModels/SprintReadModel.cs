namespace Tactify.Core.ReadModels.SprintReadModels
{
    public sealed class SprintReadModel : ReadModel
    {
        public string BoardId { get; set; }

        public string SprintId { get; set; }

        public string Status { get; set; }
    }
}
