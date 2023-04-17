using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.ActivityReadModels.Repositories
{
    public interface IActivityReadModelRepository : IProjectionRepository
    {
        Task SaveActivityReadModelAsync(ActivityReadModel activityReadModel);

        Task<IEnumerable<ActivityReadModel>> GetActivityReadModelsAsync();
    }
}
