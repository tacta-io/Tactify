using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.BoardReadModels.Repositories
{
    public interface IBoardReadModelRepository : IProjectionRepository
    {
        Task SaveBoardReadModelAsync(BoardReadModel boardReadModel);

        Task ArchiveBoardReadModelAsync(string boardId);

        Task<IEnumerable<BoardReadModel>> GetBoardReadModelsAsync();
    }
}
