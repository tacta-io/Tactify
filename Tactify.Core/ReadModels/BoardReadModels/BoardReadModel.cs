namespace Tactify.Core.ReadModels.BoardReadModels
{
    public sealed class BoardReadModel
    {
        public int Sequence { get; set; }

        public string BoardId { get; set; }

        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
}
