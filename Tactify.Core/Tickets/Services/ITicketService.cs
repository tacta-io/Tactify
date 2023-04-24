using Tactify.Core.Boards.Entities;
using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Core.Tickets.Services
{
    public interface ITicketService
    {
        Task OpenTicket(TicketInfo ticketInfo);

        Task EstimateTicket(TicketId ticketId, int numberOfDays, string createdBy);

        Task MoveTicketToSprint(TicketId ticketId, SprintId sprintId, string createdBy);

        Task AssignTicket(TicketId ticketId, string assignee, string createdBy);

        Task CloseTicket(TicketId ticketId, string createdBy);
    }
}
