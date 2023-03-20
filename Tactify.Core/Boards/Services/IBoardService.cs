using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Boards.Services
{
    public interface IBoardService
    {
        Task CreateBoard(BoardInformation boardInformation);
        Task CreateNewSprint(BoardId boardId);
    }
}
