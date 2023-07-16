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

        public async Task<TicketReadModel> GetTicketReadModel(string ticketId)
        {
            return await _ticketReadModelRepository.GetAsync(ticketId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TicketReadModel>> GetTicketReadModels(string boardId, string sprintId)
        {
            return await _ticketReadModelRepository.GetAsync(boardId, sprintId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TicketReadModel>> GetBacklog(string boardId)
        {
            return await _ticketReadModelRepository.GetBacklogAsync(boardId).ConfigureAwait(false);
        }
    }
}
