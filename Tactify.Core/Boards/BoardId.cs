using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards
{
    public sealed class BoardId : EntityId
    {
        public string BoardName { get; }

        public BoardId(string boardName)
        {
            BoardName = boardName;
        }

        public static BoardId Identity(string boardId)
        {
            var values = boardId.Split("-");

            return new BoardId(values[1]);
        }

        public override string ToString()
        {
            return $"Board-{BoardName}";
        }
    }
}
