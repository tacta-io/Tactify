using Tacta.EventStore.Repository;

namespace Tactify.Core.ReadModels.ActivityReadModels.Repositories
{
    public interface IActivityReadModelRepository : IProjectionRepository
    {
        Task InsertAsync(ActivityReadModel activityReadModel);

        Task<IEnumerable<ActivityReadModel>> GetAsync();
    }
}
