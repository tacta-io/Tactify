using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Api.Models
{
    public class CreateBoardRequest
    {
        public string Identifier { get; }

        public string Description { get; }

        public CreateBoardRequest(string identifier, string description)
        {
            Identifier = identifier;
            Description = description;
        }

        public BoardInformation ToBoardInformation(string createdBy) => new BoardInformation(Identifier, Description, createdBy);        
    }
}
