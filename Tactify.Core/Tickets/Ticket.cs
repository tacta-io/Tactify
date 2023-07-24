using Tacta.EventStore.Domain;
using Tactify.Core.Boards.Entities;
using Tactify.Core.Tickets.DomainEvents;
using Tactify.Core.Tickets.Exceptions;
using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Core.Tickets
{
    public sealed class Ticket : AggregateRoot<TicketId>
    {
        public override TicketId Id { get; protected set; }

        private bool IsClosed { get; set; } = false;

        private bool IsEstimated { get; set; } = false;


        private Ticket() 
        {

        }

        public Ticket(IEnumerable<IDomainEvent> events) : base(events) 
        {

        }


        public static Ticket OpenTicket(TicketInfo ticketInfo)
        {
            var ticket = new Ticket();

            var ticketId = new TicketId(ticketInfo.BoardId.BoardName, ticketInfo.TicketNumber);

            var @event = new TicketOpened(ticketId.ToString(), ticketInfo.BoardId.ToString(), ticketInfo.Description, ticketInfo.CreatedBy);

            ticket.Apply(@event);

            return ticket;
        }

        public void EstimateTicket(int numberOfDays, string createdBy)
        {
            if (IsClosed) throw new CannotEstimateTicketException($"Closed ticket {Id} can not be changed.");

            if (numberOfDays < 0) throw new CannotEstimateTicketException($"Estimation {numberOfDays} in not valid.");

            var @event = new TicketEstimated(Id.ToString(), numberOfDays, createdBy);

            Apply(@event);
        }

        public void MoveTicketToSprint(SprintId sprintId, string createdBy)
        {
            if (IsClosed) throw new CannotMoveTicketToSprintException($"Closed ticket {Id} can not be moved to a sprint.");

            if (!IsEstimated) throw new CannotMoveTicketToSprintException($"Ticket {Id} needs to be estimated first.");

            if (sprintId == null) throw new CannotMoveTicketToSprintException($"Missing sprint to move the ticket {Id} to.");

            var @event = new TicketMovedToSprint(Id.ToString(), sprintId.ToString(), createdBy);

            Apply(@event);
        }      

        public void AssignTicket(string assignee, string createdBy)
        {
            if (IsClosed) throw new CannotAssignTicketException($"Closed ticket {Id} can not be changed.");

            if (string.IsNullOrWhiteSpace(assignee)) throw new CannotAssignTicketException($"Invalid assignee {assignee}.");

            var @event = new TicketAssigned(Id.ToString(), assignee, createdBy);

            Apply(@event);
        }

        public void CloseTicket(string createdBy)
        {
            if (IsClosed) throw new CannotCloseTicketException($"Ticket {Id} is already closed.");

            var @event = new TicketClosed(Id.ToString(), createdBy);

            Apply(@event);
        }


        public void On(TicketOpened @event)
        {
            Id = TicketId.Identity(@event.AggregateId);
        }

        public void On(TicketEstimated _)
        {
            IsEstimated = true;
        }

        public void On(TicketMovedToSprint _) 
        { 

        }       

        public void On(TicketAssigned _)
        {

        }

        public void On(TicketClosed _)
        {
            IsClosed = true;
        }
    }
}
