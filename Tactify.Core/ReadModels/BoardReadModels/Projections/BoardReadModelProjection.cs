using Tacta.EventStore.Projector;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.ReadModels.BoardReadModels.Repositories;

namespace Tactify.Core.ReadModels.BoardReadModels.Projections
{
    public sealed class BoardReadModelProjection : Projection
    {
        private readonly IBoardReadModelRepository _boardReadModelRepository;

        public BoardReadModelProjection(IBoardReadModelRepository boardReadModelRepository) : base(boardReadModelRepository)
        {
            _boardReadModelRepository = boardReadModelRepository;
        }

        public async Task On(BoardCreated @event)
        {
            var board = new BoardReadModel
            { 
                Sequence = @event.Sequence,
                BoardId = @event.AggregateId,
                Description = @event.Description,
                IsArchived = false
            };

            await _boardReadModelRepository.OnBoardCreatedAsync(board).ConfigureAwait(false);
        }

        public async Task On(BoardArchived @event)
        {
            var board = new BoardReadModel
            {
                Sequence = @event.Sequence,
                BoardId = @event.AggregateId,
                IsArchived = true
            };

            await _boardReadModelRepository.OnBoardArchivedAsync(board).ConfigureAwait(false);
        }
    }
}
