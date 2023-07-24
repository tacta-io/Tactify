using Tactify.Core.Boards.Entities;
using Tactify.Core.Boards.Repositories;
using Tactify.Core.Tickets.Exceptions;
using Tactify.Core.Tickets.Repositories;
using Tactify.Core.Tickets.ValueObjects;

namespace Tactify.Core.Tickets.Services
{
    public sealed class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        private readonly IBoardRepository _boardRepository;

        public TicketService(ITicketRepository ticketRepository, IBoardRepository boardRepository)
        {
            _ticketRepository = ticketRepository;
            _boardRepository = boardRepository;
        }

        public async Task OpenTicket(TicketInfo ticketInfo)
        {
            var board = await _boardRepository.GetAsync(ticketInfo.BoardId).ConfigureAwait(false);

            if (board.IsArchived) throw new CannotOpenTicketException($"Board {ticketInfo.BoardId} is archived.");

            var ticketNumber = await _ticketRepository.GetNextTicketNumberAsync().ConfigureAwait(false);

            var ticket = Ticket.OpenTicket(ticketInfo.WithTicketNumber(ticketNumber));

            await _ticketRepository.SaveAsync(ticket).ConfigureAwait(false);
        }     

        public async Task EstimateTicket(TicketId ticketId, int numberOfDays, string createdBy)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId).ConfigureAwait(false);

            ticket.EstimateTicket(numberOfDays, createdBy);

            await _ticketRepository.SaveAsync(ticket).ConfigureAwait(false);
        }

        public async Task MoveTicketToSprint(TicketId ticketId, SprintId sprintId, string createdBy)
        {
            var board = await _boardRepository.GetAsync(ticketId.BoardId).ConfigureAwait(false);

            if (board.SprintById(sprintId.ToString()).IsEnded) throw new CannotMoveTicketToSprintException($"Sprint {sprintId} ended.");

            var ticket = await _ticketRepository.GetAsync(ticketId).ConfigureAwait(false);

            ticket.MoveTicketToSprint(sprintId, createdBy);

            await _ticketRepository.SaveAsync(ticket).ConfigureAwait(false);
        }

        public async Task AssignTicket(TicketId ticketId, string assignee, string createdBy)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId).ConfigureAwait(false);

            ticket.AssignTicket(assignee, createdBy);

            await _ticketRepository.SaveAsync(ticket).ConfigureAwait(false);
        }

        public async Task CloseTicket(TicketId ticketId, string createdBy)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId).ConfigureAwait(false);

            ticket.CloseTicket(createdBy);

            await _ticketRepository.SaveAsync(ticket).ConfigureAwait(false);
        }
    }
}
