using Tacta.EventStore.Domain;
using Tactify.Core.Boards;

namespace Tactify.Core.Tickets.ValueObjects
{
    public sealed class TicketInfo : ValueObject
    {
        public BoardId BoardId { get; }

        public int TicketNumber { get; private set; }

        public string Description { get; }

        public string CreatedBy { get; }


        public TicketInfo(BoardId boardId, string description, string createdBy, int ticketNumber = 0)
        {
            if (boardId == null) throw new ArgumentException("Ticket needs to belong to a board.");          

            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Ticket description is mandatory.");

            BoardId = boardId;
            TicketNumber = ticketNumber;
            Description = description;
            CreatedBy = createdBy;
        }

        public TicketInfo WithTicketNumber(int ticketNumber)
        {
            if (ticketNumber <= 0) throw new ArgumentException("Ticket needs to have valid ticket number.");
           
            return new TicketInfo(BoardId, Description, CreatedBy, ticketNumber);
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
