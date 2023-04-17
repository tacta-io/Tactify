using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.SprintReadModels.Repositories
{
    public interface ISprintReadModelRepository : IProjectionRepository
    {

        Task SaveSprintReadModelAsync(SprintReadModel sprintReadModel);

        Task UpdateSprintReadModelStatusAsync(string boardId, string sprintId, string status);

        Task<IEnumerable<SprintReadModel>> GetSprintReadModelsAsync(string boardId);
    }
}
