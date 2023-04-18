using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Boards.Services
{
    public interface IBoardService
    {
        Task CreateBoard(BoardInfo boardInfo);

        Task CreateNewSprint(BoardId boardId, string createdBy);

        Task StartNextSprint(BoardId boardId, string createdBy);

        Task EndActiveSprint(BoardId boardId, string createdBy);

        Task ArchiveBoard(BoardId boardId, string createdBy);
    }
}
