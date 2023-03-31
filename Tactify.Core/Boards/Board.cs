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


        public static Board CreateBoard(BoardInformation boardInformation)
        { 
            var board = new Board();

            var boardId = new BoardId(boardInformation.Identifier);

            var @event = new BoardCreated(boardId.ToString(), boardInformation.Description);

            board.Apply(@event);

            return board;
        }

        public void CreateNewSprint()
        {
            var lastSprintNumber = _sprints.LastOrDefault()?.Id.SprintNumber ?? 0;
            var sprintId = new SprintId(lastSprintNumber + 1);
            var @event = new SprintCreated(Id.ToString(), sprintId.ToString());
            
            Apply(@event);
        }

        public void StartNextSprint()
        {
            var isThereActiveSprint = _sprints.Any(x => x.Status == SprintStatus.Active);

            if (isThereActiveSprint) throw new Exception("There is an active sprint already.");

            var sprintToStart = _sprints.FirstOrDefault(x => x.Status == SprintStatus.Created);

            if (sprintToStart == null) throw new Exception("No created sprints to start.");

            var @event = new SprintStarted(Id.ToString(), sprintToStart.Id.ToString());

            Apply(@event);
        }

        public void EndActiveSprint()
        {
            
        }

        public void ArchiveBoard()
        {
            
        }

        public void On(BoardCreated @event)
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
            var sprint = _sprints.Single(x => x.Id.ToString() == @event.SprintId);

            sprint.StartSprint();
        }

        public void On(SprintEnded @event)
        {
            
        }

        public void On(BoardArchived _)
        {
            
        }
    }
}
