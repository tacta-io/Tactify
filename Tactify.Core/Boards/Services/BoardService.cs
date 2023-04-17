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

        public async Task CreateBoard(BoardInformation boardInformation) 
        {
            var board = Board.CreateBoard(boardInformation);

            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }

        public async Task CreateNewSprint(BoardId boardId, string createdBy)
        { 
            var board = await _boardRepository.GetAsync(boardId).ConfigureAwait(false);

            board.CreateNewSprint(createdBy);

            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }

        public async Task StartNextSprint(BoardId boardId, string createdBy)
        {
            var board = await _boardRepository.GetAsync(boardId).ConfigureAwait(false);

            board.StartNextSprint(createdBy);

            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }

        public async Task EndActiveSprint(BoardId boardId, string createdBy)
        {
            var board = await _boardRepository.GetAsync(boardId).ConfigureAwait(false);

            board.EndActiveSprint(createdBy);

            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }

        public async Task ArchiveBoard(BoardId boardId, string createdBy)
        {
            var board = await _boardRepository.GetAsync(boardId).ConfigureAwait(false);

            board.ArchiveBoard(createdBy);

            await _boardRepository.SaveAsync(board).ConfigureAwait(false);
        }
    }
}
