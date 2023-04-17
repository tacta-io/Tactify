namespace Tactify.Core.ReadModels.BoardReadModels
{
    public sealed class BoardReadModel : ReadModel
    {
        public string BoardId { get; set; }

        public string Description { get; set; }

        public bool IsArchived { get; set; }

    }
}
