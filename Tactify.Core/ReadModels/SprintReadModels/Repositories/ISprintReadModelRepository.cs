using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.SprintReadModels.Repositories
{
    public interface ISprintReadModelRepository : IProjectionRepository
    {
        Task<IEnumerable<SprintReadModel>> GetAsync(string boardId);

        Task OnSprintCreatedAsync(SprintReadModel sprintReadModel);

        Task OnSprintStartedAsync(SprintReadModel sprintReadModel);

        Task OnSprintEndedAsync(SprintReadModel sprintReadModel);        
    }
}
