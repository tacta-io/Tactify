namespace Tactify.Core.ReadModels
{
    public sealed class BoardReadModel : ReadModel
    {
        public string BoardId { get; }
        public string Description { get; }

        public BoardReadModel(
            DateTime updatedAt, 
            int sequence, 
            Guid eventId, 
            string boardId, 
            string description) : base(updatedAt, sequence, eventId)
        {
            BoardId = boardId;
            Description = description;
        }
    }
}
