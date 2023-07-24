using Tacta.EventStore.Domain;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.Boards.Entities;
using Tactify.Core.Boards.Exceptions;
using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Boards
{
    public sealed class Board : AggregateRoot<BoardId>
    {
        public override BoardId Id { get; protected set; }

        public bool IsArchived { get; private set; } = false;

        private List<Sprint> Sprints { get; set; } = new List<Sprint>();

        private Sprint? ActiveSprint => Sprints.SingleOrDefault(x => x.Status == SprintStatus.Active);

        private Sprint? NextSprintToStart => Sprints.OrderBy(x => x.Id.SprintNumber).FirstOrDefault(x => x.Status == SprintStatus.Created);

        private int NewSprintNumber => Sprints.Any() ? Sprints.Max(x => x.Id.SprintNumber) + 1 : 1;

        public Sprint SprintById(string sprintId) => Sprints.Single(x => x.Id.ToString() == sprintId);


        private Board() 
        {

        }

        public Board(IEnumerable<IDomainEvent> events) : base(events) 
        { 

        }


        public static Board CreateBoard(BoardInfo boardInfo)
        { 
            var board = new Board();

            var boardId = new BoardId(boardInfo.Name);

            var @event = new BoardCreated(boardId.ToString(), boardInfo.Description, boardInfo.CreatedBy);

            board.Apply(@event);

            return board;
        }

        public void CreateNewSprint(string createdBy)
        {
            if (IsArchived) throw new CannotCreateNewSprintException($"Board {Id} is archived.");           

            var sprintId = new SprintId(NewSprintNumber);

            var @event = new SprintCreated(Id.ToString(), sprintId.ToString(), createdBy);

            Apply(@event);
        }

        public void StartNextSprint(string createdBy)
        {
            if (IsArchived) throw new CannotStartNextSprintException($"Board {Id} is archived.");

            if (ActiveSprint != null) throw new CannotStartNextSprintException($"There is already active sprint {ActiveSprint.Id}.");

            if (NextSprintToStart == null) throw new CannotStartNextSprintException($"There is no created sprint to start.");

            var @event = new SprintStarted(Id.ToString(), NextSprintToStart.Id.ToString(), createdBy);

            Apply(@event);
        }

        public void EndActiveSprint(string createdBy)
        {
            if (IsArchived) throw new CannotEndActiveSprintException($"Board {Id} is archived.");

            if (ActiveSprint == null) throw new CannotEndActiveSprintException($"There is no active sprint to end.");

            var @event = new SprintEnded(Id.ToString(), ActiveSprint.Id.ToString(), createdBy);

            Apply(@event);
        }

        public void ArchiveBoard(string createdBy)
        {
            if (IsArchived) throw new CannotArchiveBoardException($"Board {Id} is already archived.");

            if (Sprints.Any(x => x.Status != SprintStatus.Ended)) throw new CannotArchiveBoardException($"Not all sprints ended on the board {Id}.");

            var @event = new BoardArchived(Id.ToString(), createdBy);

            Apply(@event);
        }


        public void On(BoardCreated @event)
        {
            Id = BoardId.Identity(@event.AggregateId);
        }

        public void On(SprintCreated @event)
        {
            var sprintId = SprintId.Identity(@event.SprintId);

            var sprint = new Sprint(sprintId);

            Sprints.Add(sprint);
        }

        public void On(SprintStarted @event)
        {
            var sprint = SprintById(@event.SprintId);

            sprint.SprintStarted();
        }

        public void On(SprintEnded @event)
        {
            var sprint = SprintById(@event.SprintId);

            sprint.SprintEnded();
        }

        public void On(BoardArchived _)
        {
            IsArchived = true;
        }
    }
}
