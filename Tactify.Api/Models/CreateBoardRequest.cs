using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Api.Models
{
    public class CreateBoardRequest
    {
        public string Name { get; }

        public string Description { get; }

        public CreateBoardRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public BoardInfo ToBoardInfo(string createdBy) => new BoardInfo(Name, Description, createdBy);        
    }
}
