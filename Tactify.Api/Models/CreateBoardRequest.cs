using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Api.Models
{
    public class CreateBoardRequest
    {
        public string Identifier { get; set; }
        public string Description { get; set; }

        public BoardInformation ToBoardInformation() => new BoardInformation(Identifier, Description);        
    }
}
