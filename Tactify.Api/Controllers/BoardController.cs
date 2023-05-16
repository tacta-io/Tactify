using Microsoft.AspNetCore.Mvc;
using Tactify.Api.Models;
using Tactify.Core.Boards;
using Tactify.Core.Boards.Services;
using Tactify.Core.ReadModels.BoardReadModels.Services;
using Tactify.Core.ReadModels.SprintReadModels.Services;
using Tactify.Core.ReadModels.TicketReadModels.Services;

namespace Tactify.Api.Controllers
{

    [Route("api/board")]
    public class BoardController : BaseController
    {
        private readonly IBoardService _boardService;
        private readonly IBoardReadModelService _boardReadModelService;
        private readonly ISprintReadModelService _sprintReadModelService;
        private readonly ITicketReadModelService _ticketReadModelService;

        public BoardController(
            IBoardService boardService, 
            IBoardReadModelService boardReadModelService,
            ISprintReadModelService sprintReadModelService,
            ITicketReadModelService ticketReadModelService)
        {
            _boardService = boardService;
            _boardReadModelService = boardReadModelService;
            _sprintReadModelService = sprintReadModelService;
            _ticketReadModelService = ticketReadModelService;
        }

        [HttpPost]
        [Route("create-board")]
        public async Task<ActionResult> CreateBoard([FromBody] CreateBoardRequest createBoardRequest)
        {
            await _boardService.CreateBoard(createBoardRequest.ToBoardInfo(Username)).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/create-sprint")]
        public async Task<ActionResult> CreateNewSprint([FromRoute] string boardId)
        {
            await _boardService.CreateNewSprint(BoardId.Identity(boardId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/start-next-sprint")]
        public async Task<ActionResult> StartNextSprint([FromRoute] string boardId)
        {
            await _boardService.StartNextSprint(BoardId.Identity(boardId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/end-active-sprint")]
        public async Task<ActionResult> EndActiveSprint([FromRoute] string boardId)
        {
            await _boardService.EndActiveSprint(BoardId.Identity(boardId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/archive-board")]
        public async Task<ActionResult> ArchiveBoard([FromRoute] string boardId)
        {
            await _boardService.ArchiveBoard(BoardId.Identity(boardId), Username).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetBoards()
        {
            var boards = await _boardReadModelService.GetBoardReadModels().ConfigureAwait(false);

            return Ok(boards);
        }

        [HttpGet]
        [Route("{boardId}/sprints")]
        public async Task<ActionResult> GetSprints([FromRoute] string boardId)
        {
            var sprints = await _sprintReadModelService.GetSprintReadModels(boardId).ConfigureAwait(false);

            return Ok(sprints);
        }

        [HttpGet]
        [Route("{boardId}/backlog")]
        public async Task<ActionResult> GetBacklog([FromRoute] string boardId)
        {
            var tickets = await _ticketReadModelService.GetTicketReadModels(boardId, null).ConfigureAwait(false);

            return Ok(tickets);
        }

        [HttpGet]
        [Route("{boardId}/{sprintId}/tickets")]
        public async Task<ActionResult> GetTickets([FromRoute] string boardId, [FromRoute] string sprintId)
        {
            var tickets = await _ticketReadModelService.GetTicketReadModels(boardId, sprintId).ConfigureAwait(false);

            return Ok(tickets);
        }
    }
}