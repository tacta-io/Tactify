using Microsoft.AspNetCore.Mvc;
using Tactify.Core.ReadModels.ActivityReadModels.Services;

namespace Tactify.Api.Controllers
{
    [Route("api/activity")]
    public class ActivityController : BaseController
    {
        private readonly IActivityReadModelService _activityReadModelService;

        public ActivityController(IActivityReadModelService activityReadModelService)
        {
            _activityReadModelService = activityReadModelService;
        }


        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetActivities()
        {
            var activities = await _activityReadModelService.GetActivityReadModels().ConfigureAwait(false);

            return Ok(activities);
        }
    }
}
