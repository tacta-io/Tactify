using Tactify.Core.ReadModels.ActivityReadModels.Repositories;

namespace Tactify.Core.ReadModels.ActivityReadModels.Services
{
    public sealed class ActivityReadModelService : IActivityReadModelService
    {
        private readonly IActivityReadModelRepository _activityReadModelRepository;

        public ActivityReadModelService(IActivityReadModelRepository activityReadModelRepository)
        {
            _activityReadModelRepository = activityReadModelRepository;
        }

        public async Task<IEnumerable<ActivityReadModel>> GetActivityReadModels()
        {
            return await _activityReadModelRepository.GetAsync().ConfigureAwait(false);
        }
    }
}
