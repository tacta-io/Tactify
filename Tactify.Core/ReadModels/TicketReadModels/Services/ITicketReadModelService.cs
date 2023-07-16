namespace Tactify.Core.ReadModels.TicketReadModels.Services
{
    public interface ITicketReadModelService
    {

        Task<TicketReadModel> GetTicketReadModel(string ticketId);

        Task<IEnumerable<TicketReadModel>> GetTicketReadModels(string boardId, string sprintId);

        Task<IEnumerable<TicketReadModel>> GetBacklog(string boardId);
    }
}
