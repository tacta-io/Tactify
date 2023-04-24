using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.TicketReadModels.Repositories
{
    public interface ITicketReadModelRepository : IProjectionRepository
    {
        Task<IEnumerable<TicketReadModel>> GetAsync(string boardId, string? sprintId);

        Task OnTicketOpenedAsync(TicketReadModel ticketReadModel);

        Task OnTicketEstimatedAsync(TicketReadModel ticketReadModel);

        Task OnTicketMovedToSprintAsync(TicketReadModel ticketReadModel);

        Task OnTicketAssignedAsync(TicketReadModel ticketReadModel);

        Task OnTicketClosedAsync(TicketReadModel ticketReadModel);
    }
}
