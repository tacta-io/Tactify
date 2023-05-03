using Tactify.Core.ReadModels.ActivityReadModels;

namespace Tactify.Api.Models
{
    public class GetActivitiesResponse
    {
        public DateTime CreatedAt { get; }

        public string CreatedBy { get; }

        public string Name { get; }

        public string Description { get; }

        public GetActivitiesResponse(ActivityReadModel activityReadModel)
        {
            CreatedAt = activityReadModel.CreatedAt;
            CreatedBy = activityReadModel.CreatedBy;
            Name = activityReadModel.Name;
            Description = activityReadModel.Description;
        }
    }
}
