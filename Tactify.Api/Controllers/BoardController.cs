using Microsoft.AspNetCore.Mvc;
using Tactify.Api.Models;
using Tactify.Core.Boards.Services;

namespace Tactify.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost(Name = "OpenBoard")]     
        public async Task<ActionResult> OpenBoard([FromBody] OpenBoardRequest openBoardRequest)
        {
            var boardInformation = openBoardRequest.ToBoardInformation();
            await _boardService.OpenBoard(boardInformation).ConfigureAwait(false);
            return Ok();
        }
    }
}