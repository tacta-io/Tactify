using Tacta.EventStore.Projector;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.ReadModels.Repositories;

namespace Tactify.Core.ReadModels.Projections
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
            var board = new BoardReadModel(
                @event.CreatedAt,
                @event.Sequence, 
                @event.Id,
                @event.AggregateId,
                @event.Description);         

            await _boardReadModelRepository.InsertAsync(board).ConfigureAwait(false);
        }
    }
}
