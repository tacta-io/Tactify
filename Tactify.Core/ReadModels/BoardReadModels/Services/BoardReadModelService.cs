using Tactify.Core.ReadModels.BoardReadModels.Repositories;

namespace Tactify.Core.ReadModels.BoardReadModels.Services
{
    public sealed class BoardReadModelService : IBoardReadModelService
    {
        private readonly IBoardReadModelRepository _boardReadModelRepository;

        public BoardReadModelService(IBoardReadModelRepository boardReadModelRepository)
        {
            _boardReadModelRepository = boardReadModelRepository;
        }

        public async Task<IEnumerable<BoardReadModel>> GetBoardReadModels()
        {
            return await _boardReadModelRepository.GetAsync().ConfigureAwait(false);
        }
    }
}
