using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.BoardReadModels.Repositories
{
    public interface IBoardReadModelRepository : IProjectionRepository
    {
        Task<IEnumerable<BoardReadModel>> GetAsync();

        Task OnBoardCreatedAsync(BoardReadModel boardReadModel);

        Task OnBoardArchivedAsync(BoardReadModel boardReadModel);        
    }
}
