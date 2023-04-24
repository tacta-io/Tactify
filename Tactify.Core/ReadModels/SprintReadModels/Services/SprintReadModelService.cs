using Tactify.Core.ReadModels.SprintReadModels.Repositories;

namespace Tactify.Core.ReadModels.SprintReadModels.Services
{
    public sealed class SprintReadModelService : ISprintReadModelService
    {
        private readonly ISprintReadModelRepository _sprintReadModelRepository;

        public SprintReadModelService(ISprintReadModelRepository sprintReadModelRepository)
        {
            _sprintReadModelRepository = sprintReadModelRepository;
        }

        public async Task<IEnumerable<SprintReadModel>> GetSprintReadModels(string boardId)
        {
            return await _sprintReadModelRepository.GetAsync(boardId).ConfigureAwait(false);
        }
    }
}
