using Tactify.Core.ReadModels.BoardReadModels;

namespace Tactify.Api.Models
{
    public class GetBoardsResponse
    {
        public string BoardId { get; }     

        public string Description { get; }

        public bool IsArchived { get; }

        public GetBoardsResponse(BoardReadModel boardReadModel)
        {
            BoardId = boardReadModel.BoardId;          
            Description = boardReadModel.Description;
            IsArchived = boardReadModel.IsArchived;
        }
    }
}
