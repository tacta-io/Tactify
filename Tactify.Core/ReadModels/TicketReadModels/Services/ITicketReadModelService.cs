namespace Tactify.Core.ReadModels.TicketReadModels.Services
{
    public interface ITicketReadModelService
    {
        Task<IEnumerable<TicketReadModel>> GetTicketReadModels(string boardId, string? sprintId);
    }
}
