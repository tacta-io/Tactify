using Tacta.EventStore.Domain;
using Tactify.Core.Boards.Entities;
using Tactify.Core.Tickets.DomainEvents;
using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Core.Tickets
{
    public sealed class Ticket : AggregateRoot<TicketId>
    {
        public override TicketId Id { get; protected set; }


        public static Ticket CreateTicket(string boardIdenetifer, int ticketNumber, string description)
        {
            var ticket = new Ticket();

            var ticketId = new TicketId(boardIdenetifer, ticketNumber);

            var @event = new TicketCreated(ticketId.ToString(), description);

            ticket.Apply(@event);

            return ticket;
        }

        public void MoveTicketToSprint(SprintId sprintId)
        {
            var @event = new TicketMovedToSprint(Id.ToString(), sprintId.ToString());

            Apply(@event);
        }

        public void EstimateTicket(Estimation estimation)
        {
            var @event = new TicketEstimated(Id.ToString(), estimation.NumberOfUnits, estimation.MeasurementUnit);

            Apply(@event);
        }

        public void AssignTicket(string assignee)
        {
            var @event = new TicketAssigned(Id.ToString(), assignee);

            Apply(@event);
        }

        public void CloseTicket()
        {
            var @event = new TicketClosed(Id.ToString());

            Apply(@event);
        }

        public void On(TicketCreated @event)
        {
            Id = TicketId.Identity(@event.AggregateId);
        }

        public void On(TicketMovedToSprint @event) 
        { 

        }

        public void On(TicketEstimated @event) 
        { 

        }

        public void On(TicketAssigned @event)
        {

        }

        public void On(TicketClosed @event)
        {

        }
    }
}
