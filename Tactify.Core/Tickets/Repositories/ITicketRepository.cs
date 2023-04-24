namespace Tactify.Core.Tickets.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetAsync(TicketId ticketId);

        Task SaveAsync(Ticket ticket);

        Task<int> GetNextTicketNumber();
    }
}
