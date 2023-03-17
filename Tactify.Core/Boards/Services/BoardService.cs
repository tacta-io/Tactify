using Tactify.Core.Boards.Repositories;
using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Boards.Services
{
    public sealed class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository) 
        {
            _boardRepository = boardRepository;
        }

        public async Task OpenBoard(BoardInformation boardInformation) 
        {
            var board = Board.OpenBoard(boardInformation);
            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }

        public async Task CreateNewSprint(BoardId boardId)
        { 
            var board = await _boardRepository.GetAsync(boardId).ConfigureAwait(false);
            board.CreateNewSprint();
            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }
    }
}
