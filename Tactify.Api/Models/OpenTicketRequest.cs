using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Api.Models
{
    public class OpenTicketRequest
    {
        public string BoardId { get; }

        public string Description { get; }

        public OpenTicketRequest(string boardId, string description)
        {
            BoardId = boardId;
            Description = description;
        }

        public TicketInfo ToTicketInfo(string createdBy) => new(Core.Boards.BoardId.Identity(BoardId), Description, createdBy);
    }
}
