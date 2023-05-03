namespace Tactify.Core.ReadModels.ActivityReadModels.Services
{
    public interface IActivityReadModelService
    {
        Task<IEnumerable<ActivityReadModel>> GetActivityReadModels();
    }
}
