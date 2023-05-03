using Tactify.Core.ReadModels.SprintReadModels;

namespace Tactify.Api.Models
{
    public class GetSprintsResponse
    {
        public string SprintId { get; }

        public string Status { get; }

        public DateTime? StartedAt { get; }

        public DateTime? EndedAt { get; }

        public GetSprintsResponse(SprintReadModel sprintReadModel)
        {
            SprintId = sprintReadModel.SprintId;
            Status = sprintReadModel.Status;
            StartedAt = sprintReadModel.StartedAt;
            EndedAt = sprintReadModel.EndedAt;
        }
    }
}
