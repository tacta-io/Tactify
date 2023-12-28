using Microsoft.AspNetCore.Mvc;
using Tacta.EventStore.Projector;

namespace Tactify.Projections.Controllers
{
    [Route("status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IProjectionProcessor? _projectionProcessor;

        public StatusController(IServiceProvider serviceProvider)
        {
            _projectionProcessor = serviceProvider.GetService(typeof(IProjectionProcessor)) as IProjectionProcessor;
        }

        [HttpGet]
        public async Task<ContentResult> Get()
        {
            if (_projectionProcessor == null) return base.Content("No data", "text/html");

            var content = await _projectionProcessor.Status("Tactify").ConfigureAwait(false);

            return base.Content(content, "text/html");
        }
    }
}
