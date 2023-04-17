using Microsoft.AspNetCore.Mvc;
using Tactify.Api.Models;
using Tactify.Core.Boards;
using Tactify.Core.Boards.Services;
using Tactify.Core.ReadModels.BoardReadModels.Services;
using Tactify.Core.ReadModels.SprintReadModels.Services;

namespace Tactify.Api.Controllers
{
    [ApiController]
    [Route("api/board")]
    public class BoardController : ControllerBase
    {
        private readonly string _username = "TactifyUser";

        private readonly IBoardService _boardService;
        private readonly IBoardReadModelService _boardReadModelService;
        private readonly ISprintReadModelService _sprintReadModelService;

        public BoardController(
            IBoardService boardService, 
            IBoardReadModelService boardReadModelService,
            ISprintReadModelService sprintReadModelService)
        {
            _boardService = boardService;
            _boardReadModelService = boardReadModelService;
            _sprintReadModelService = sprintReadModelService;
        }

        [HttpPost]
        [Route("create-board")]
        public async Task<ActionResult> CreateBoard([FromBody] CreateBoardRequest createBoardRequest)
        {
            await _boardService.CreateBoard(createBoardRequest.ToBoardInformation(_username)).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/create-sprint")]
        public async Task<ActionResult> CreateNewSprint([FromRoute] string boardId)
        {
            await _boardService.CreateNewSprint(new BoardId(boardId), _username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/start-next-sprint")]
        public async Task<ActionResult> StartNextSprint([FromRoute] string boardId)
        {
            await _boardService.StartNextSprint(new BoardId(boardId), _username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/end-active-sprint")]
        public async Task<ActionResult> EndActiveSprint([FromRoute] string boardId)
        {
            await _boardService.EndActiveSprint(new BoardId(boardId), _username).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Route("{boardId}/archive-board")]
        public async Task<ActionResult> ArchiveBoard([FromRoute] string boardId)
        {
            await _boardService.ArchiveBoard(new BoardId(boardId), _username).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetBoards()
        {
            var boards = await _boardReadModelService.GetBoardReadModels().ConfigureAwait(false);

            return Ok(boards.Select(x => new GetBoardsResponse(x)));
        }

        [HttpGet]
        [Route("{boardId}/sprints")]
        public async Task<ActionResult> GetSprints([FromRoute] string boardId)
        {
            var sprints = await _sprintReadModelService.GetSprintReadModels(boardId).ConfigureAwait(false);

            return Ok(sprints.Select(x => new GetSprintsResponse(x)));
        }
    }
}