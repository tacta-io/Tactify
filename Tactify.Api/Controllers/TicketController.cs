using Microsoft.AspNetCore.Mvc;
using Tactify.Api.Models;
using Tactify.Core.Boards.Entities;
using Tactify.Core.ReadModels.TicketReadModels.Services;
using Tactify.Core.Tickets;
using Tactify.Core.Tickets.Services;

namespace Tactify.Api.Controllers
{
    [Route("api/ticket")]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketReadModelService _ticketReadModelService;

        public TicketController(ITicketService ticketService, ITicketReadModelService ticketReadModelService)
        {
            _ticketService = ticketService;
            _ticketReadModelService = ticketReadModelService;
        }

        [HttpPost]
        [Route("open")]
        public async Task<ActionResult> OpenTicket([FromBody] OpenTicketRequest openTicketRequest)
        {
            await _ticketService.OpenTicket(openTicketRequest.ToTicketInfo(Username)).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{ticketId}/estimate/{numberOfDays}")]
        public async Task<ActionResult> EstimateTicket([FromRoute] string ticketId, [FromRoute] int numberOfDays)
        {
            await _ticketService.EstimateTicket(TicketId.Identity(ticketId), numberOfDays, Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{ticketId}/move-to-sprint/{sprintId}")]
        public async Task<ActionResult> MoveTicketToSprint([FromRoute] string ticketId, [FromRoute] string sprintId)
        {
            await _ticketService.MoveTicketToSprint(TicketId.Identity(ticketId), SprintId.Identity(sprintId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{ticketId}/assign/{assignee}")]
        public async Task<ActionResult> AssignTicket([FromRoute] string ticketId, [FromRoute] string assignee)
        {
            await _ticketService.AssignTicket(TicketId.Identity(ticketId), assignee, Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{ticketId}/close")]
        public async Task<ActionResult> CloseTicket([FromRoute] string ticketId)
        {
            await _ticketService.CloseTicket(TicketId.Identity(ticketId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        [Route("{ticketId}")]
        public async Task<ActionResult> GetTicket([FromRoute] string ticketId)
        {
            var ticket = await _ticketReadModelService.GetTicketReadModel(ticketId).ConfigureAwait(false);

            return Ok(ticket);
        }
    }
}
