using Tacta.EventStore.Domain;
using Tactify.Core.Boards;

namespace Tactify.Core.Tickets.ValueObjects
{
    public sealed class TicketInfo : ValueObject
    {
        public BoardId BoardId { get; }

        public int TicketNumber { get; }

        public string Description { get; }

        public string CreatedBy { get; }


        public TicketInfo(BoardId boardId, int ticketNumber, string description, string createdBy)
        {
            if (boardId == null) throw new Exception("Ticket needs to belong to a board.");

            if (ticketNumber < 0) throw new Exception("Ticket needs to have valid ticket number.");

            if (string.IsNullOrWhiteSpace(description)) throw new Exception("Ticket description is mandatory.");

            BoardId = boardId;
            TicketNumber = ticketNumber;
            Description = description;
            CreatedBy = createdBy;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return BoardId;
            yield return TicketNumber;
            yield return Description;
            yield return CreatedBy;
        }
    }
}
