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
        public ContentResult Get()
        {
            if (_projectionProcessor == null) return new ContentResult { Content = "No data", ContentType = "text/html" };

            var statuses = _projectionProcessor.Status();

            var content = string.Empty;

            foreach (var status in statuses)
            {
                content += $"<p>Last event sequence {status.Value} applied on {status.Key}</p>";
            }

            return new ContentResult { Content = content, ContentType = "text/html" };
        }
    }
}
