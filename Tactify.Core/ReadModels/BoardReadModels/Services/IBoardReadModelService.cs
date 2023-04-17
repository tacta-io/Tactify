namespace Tactify.Core.ReadModels.BoardReadModels.Services
{
    public interface IBoardReadModelService
    {
        Task<IEnumerable<BoardReadModel>> GetBoardReadModels();
    }
}
