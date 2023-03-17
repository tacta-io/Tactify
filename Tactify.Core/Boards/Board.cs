using Tacta.EventStore.Domain;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.Boards.Entities;
using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Boards
{
    public sealed class Board : AggregateRoot<BoardId>
    {
        public override BoardId Id { get; protected set; }

        private bool _isArchived;

        private List<Sprint> _sprints;

        private Board() { }

        public Board(IEnumerable<IDomainEvent> events) : base(events) { }


        public static Board OpenBoard(BoardInformation boardInformation)
        { 
            var board = new Board();

            var boardId = new BoardId(boardInformation.Identifier);

            var @event = new BoardOpened(boardId.ToString(), boardInformation.Description);

            board.Apply(@event);

            return board;
        }

        public void CreateNewSprint()
        {
            var newSprintNumber = _sprints.Max(x => x.Id.SprintNumber) + 1;

            var newSprintId = new SprintId(newSprintNumber);

            var @event = new SprintCreated(Id.ToString(), newSprintId.ToString());

            Apply(@event);
        }

        public void StartNextSprint()
        {
           
        }

        public void EndActiveSprint()
        {
            
        }

        public void ArchiveBoard()
        {
            
        }

        public void On(BoardOpened @event)
        {
            Id = BoardId.Identity(@event.AggregateId);

            _sprints = new List<Sprint>();

            _isArchived = false;
        }

        public void On(SprintCreated @event)
        {
            var sprintId = SprintId.Identity(@event.SprintId);

            var sprint = new Sprint(sprintId);

            _sprints.Add(sprint);
        }

        public void On(SprintStarted @event)
        {
            
        }

        public void On(SprintEnded @event)
        {
            
        }

        public void On(BoardArchived _)
        {
            
        }
    }
}
