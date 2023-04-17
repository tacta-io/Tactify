namespace Tactify.Core.ReadModels.SprintReadModels.Services
{
    public interface ISprintReadModelService
    {
        Task<IEnumerable<SprintReadModel>> GetSprintReadModels(string boardId);
    }
}
