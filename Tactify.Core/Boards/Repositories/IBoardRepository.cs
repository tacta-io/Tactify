namespace Tactify.Core.Boards.Repositories
{
    public interface IBoardRepository
    {
        Task<Board> GetAsync(BoardId boardId);
        Task SaveAsync(Board board);
    }
}
