using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tactify.Core.Boards;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Test.Boards
{
    [TestClass]
    public class BoardTest
    {
        private readonly string _username = "TactifyUser";

        [TestMethod]        
        public void GivenValidBoardNameAndDescription_WhenCreateBoard_ThenNewBoardCreated()
        {
            // Given            
            var boardName = "TACTIFY";
            var description = "Board for managing Tactify project";

            // When
            var board = Board.CreateBoard(new BoardInfo(boardName, description, _username));

            // Then
            var @event = board.DomainEvents.Single() as BoardCreated;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-TACTIFY", @event.AggregateId);
            Assert.AreEqual(description, @event.Description);
        }

        [TestMethod]
        public void GivenInvalidDescription_WhenCreateBoard_ThenNewBoardNotCreated()
        {
            // Given            
            var boardName = "TACTIFY";
            var description = string.Empty;

            // When
            Board createBoard() => Board.CreateBoard(new BoardInfo(boardName, description, _username));

            // Then
            Assert.ThrowsException<Exception>(createBoard);
        }

        [TestMethod]
        public void GivenInvalidboardName_WhenCreateBoard_ThenNewBoardNotCreated()
        {
            // Given            
            var boardName = "";
            var description = "Board for managing Tactify project";

            // When
            Board createBoard() => Board.CreateBoard(new BoardInfo(boardName, description, _username));

            // Then
            Assert.ThrowsException<Exception>(createBoard);
        }

        [TestMethod]
        public void GivenBoardWithoutSprints_WhenCreateNewSprint_ThenFirstSprintNumberIs1()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            // When
            board.CreateNewSprint(_username);

            // Then
            var @event = board.DomainEvents.Single(x => x is SprintCreated) as SprintCreated;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-TACTIFY", @event.AggregateId);
            Assert.AreEqual("Sprint-1", @event.SprintId);
        }

        [TestMethod]
        public void GivenBoardWithExistingSprints_WhenCreateNewSprint_ThenSprintNumbersAreIncremental()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            // When
            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);

            // Then
            var events = board.DomainEvents.OfType<SprintCreated>().ToList();

            Assert.IsTrue(events.Count == 3);

            Assert.AreEqual("Sprint-1", events[0].SprintId);
            Assert.AreEqual("Sprint-2", events[1].SprintId);
            Assert.AreEqual("Sprint-3", events[2].SprintId);
        }

        [TestMethod]
        public void GivenArchivedBoard_WhenCreateNewSprint_ThenNewSprintNotCreated()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.ArchiveBoard(_username);

            // When
            void createNewSprint() => board.CreateNewSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(createNewSprint);
            Assert.IsFalse(board.DomainEvents.Any(x => x is SprintCreated));
        }

        [TestMethod]
        public void GivenBoardWith2CreatedSprints_WhenStartNextSprint_ThenFirstOneIsStarted()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);

            // When
            board.StartNextSprint(_username);

            // Then
            var @event = board.DomainEvents.Single(x => x is SprintStarted) as SprintStarted;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-TACTIFY", @event.AggregateId);
            Assert.AreEqual("Sprint-1", @event.SprintId);
        }

        [TestMethod]
        public void GivenArchivedBoard_WhenStartNextSprint_ThenNextSprintIsNotActivated()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);
            board.ArchiveBoard(_username);

            // When
            void startNextSprint() => board.StartNextSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(startNextSprint);
        }

        [TestMethod]
        public void GivenBoardWithActiveSprint_WhenStartNextSprint_ThenSecondOneIsNotStarted()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);

            // When
            void startNextSprint() => board.StartNextSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(startNextSprint);
        }


        [TestMethod]
        public void GivenBoardWithoutSprints_WhenStartNextSprint_ThenNoSprintToActivate()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            // When
            void startNextSprint() => board.StartNextSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(startNextSprint);
            Assert.IsFalse(board.DomainEvents.Any(x => x is SprintStarted));
        }

        [TestMethod]
        public void GivenBoardWithAllSprintsEnded_WhenStartNextSprint_ThenNoSprintToActivate()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);

            // When
            void startNextSprint() => board.StartNextSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(startNextSprint);
        }

        [TestMethod]
        public void GivenBoardWithActiveSprint_WhenEndActiveSprint_ThenActiveSprintEnded()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);

            // When
            board.EndActiveSprint(_username);

            // Then
            var @event = board.DomainEvents.Single(x => x is SprintEnded) as SprintEnded;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-TACTIFY", @event.AggregateId);
            Assert.AreEqual("Sprint-1", @event.SprintId);
        }

        [TestMethod]
        public void GivenArchivedBoard_WhenEndActiveSprint_ThenNoSprintsEnded()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);
            board.ArchiveBoard(_username);

            // When
            void endActiveSprint() => board.EndActiveSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(endActiveSprint);
        }

        [TestMethod]
        public void GivenBoardWithoutActiveSprint_WhenEndActiveSprint_ThenNoSprintsEnded()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);

            // When
            void endActiveSprint() => board.EndActiveSprint(_username);

            // Then
            Assert.ThrowsException<Exception>(endActiveSprint);
            Assert.IsFalse(board.DomainEvents.Any(x => x is SprintEnded));
        }

        [TestMethod]
        public void GivenBoardWithAllSprintsEnded_WhenArchiveBoard_ThenTheBoardIsArchived()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);

            // When
            board.ArchiveBoard(_username);

            // Then
            var @event = board.DomainEvents.Single(x => x is BoardArchived) as BoardArchived;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-TACTIFY", @event.AggregateId);
        }

        [TestMethod]
        public void GivenArchivedBoard_WhenArchiveBoard_ThenTheBoardCanNotBeArchivedAgain()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.ArchiveBoard(_username);

            // When
            void archiveBoard() => board.ArchiveBoard(_username);

            // Then
            Assert.ThrowsException<Exception>(archiveBoard);
        }

        [TestMethod]
        public void GivenBoardWithActiveSprint_WhenArchiveBoard_ThenTheBoardCanNotBeArchived()
        {
            // Given            
            var board = Board.CreateBoard(new BoardInfo("TACTIFY", "Board for managing Tactify project", _username));

            board.CreateNewSprint(_username);
            board.CreateNewSprint(_username);
            board.StartNextSprint(_username);
            board.EndActiveSprint(_username);
            board.StartNextSprint(_username);

            // When
            void archiveBoard() => board.ArchiveBoard(_username);

            // Then
            Assert.ThrowsException<Exception>(archiveBoard);
        }
    }
}
