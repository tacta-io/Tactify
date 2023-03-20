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

        [HttpPost(Name = "CreateBoard")]     
        public async Task<ActionResult> CreateBoard([FromBody] CreateBoardRequest createBoardRequest)
        {
            var boardInformation = createBoardRequest.ToBoardInformation();
            await _boardService.CreateBoard(boardInformation).ConfigureAwait(false);
            return Ok();
        }
    }
}