using Tactify.Core.ReadModels.TicketReadModels.Repositories;

namespace Tactify.Core.ReadModels.TicketReadModels.Services
{
    public sealed class TicketReadModelService : ITicketReadModelService
    {
        private readonly ITicketReadModelRepository _ticketReadModelRepository;

        public TicketReadModelService(ITicketReadModelRepository ticketReadModelRepository)
        {
            _ticketReadModelRepository = ticketReadModelRepository;
        }

        public async Task<IEnumerable<TicketReadModel>> GetTicketReadModels(string boardId, string? sprintId)
        {
            return await _ticketReadModelRepository.GetAsync(boardId, sprintId).ConfigureAwait(false);
        }
    }
}
