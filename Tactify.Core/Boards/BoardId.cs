using Tacta.EventStore.Domain;

namespace Tactify.Core.Boards
{
    public sealed class BoardId : EntityId
    {
        public string BoardIdentifier { get; }

        public BoardId(string boardIdentitifer)
        {
            BoardIdentifier = boardIdentitifer;
        }

        public static BoardId Identity(string ticketId)
        {
            var values = ticketId.Split("-");

            return new BoardId(values[1]);
        }

        public override string ToString()
        {
            return $"Board-{BoardIdentifier}";
        }
    }
}
