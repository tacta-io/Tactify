using System.Text;
using Microsoft.AspNetCore.Mvc;
using Tacta.EventStore.Projector;
using Tacta.EventStore.Repository;

namespace Tactify.Projections.Controllers
{
    [Route("status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IProjectionProcessor? _projectionProcessor;
        private readonly IEventStoreRepository _eventStoreRepository;

        public StatusController(IServiceProvider serviceProvider, IEventStoreRepository eventStoreRepository)
        {
            _projectionProcessor = serviceProvider.GetService(typeof(IProjectionProcessor)) as IProjectionProcessor;
            _eventStoreRepository = eventStoreRepository;
        }

        [HttpGet]
        public ContentResult Get()
        {
            if (_projectionProcessor == null) return base.Content("No data", "text/html");

            var sequence = _eventStoreRepository.GetLatestSequence().ConfigureAwait(false).GetAwaiter().GetResult();

            var statuses = _projectionProcessor.Status();

            var data = new StringBuilder();

            foreach (var status in statuses)
            {
                data.Append($"{{projection:'{status.Key}', sequence:'{status.Value}'}},");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources/status.html");
            var html = System.IO.File.ReadAllText(path);

            var content = new StringBuilder(html)
                .Replace("{sequence}", sequence.ToString())
                .Replace("{data}", data.ToString())
                .ToString();            

            return base.Content(content, "text/html");
        }
    }
}
