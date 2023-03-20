using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tactify.Core.Boards;
using Tactify.Core.Boards.DomainEvents;
using Tactify.Core.Boards.ValueObjects;

namespace Tactify.Core.Test.Boards
{
    [TestClass]
    public class BoardTest
    {
        // ========== CreateBoard ========== 
        // A new Board should be created if user provides valid board identifier and board description
        [TestMethod]        
        public void Test1()
        {
            // Given            
            var boardIdentifier = "COB";
            var description = "Constructiv Benefits";

            // When
            var board = Board.CreateBoard(new BoardInformation(boardIdentifier, description));

            // Then
            var @event = board.DomainEvents.Single() as BoardCreated;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-COB", @event.AggregateId);
            Assert.AreEqual(description, @event.Description);
        }

        // User should not be able to create a new Board if description is not provided
        [TestMethod]
        public void Test2()
        {
            // Given            
            var boardIdentifier = "COB";
            var description = string.Empty;

            // When // Then
            Assert.ThrowsException<Exception>(() => Board.CreateBoard(new BoardInformation(boardIdentifier, description)));
        }

        // User should not be able to create a new Board if board identifier is not provided
        [TestMethod]
        public void Test3()
        {
            // Given            
            var boardIdentifier = "";
            var description = "Constructiv Benefits";

            // When // Then
            Assert.ThrowsException<Exception>(() => Board.CreateBoard(new BoardInformation(boardIdentifier, description)));
        }


        // ========== CreateNewSprint ==========        
        // User should be able to create a new Sprint for a Board, and Sprint number should start from 1
        // If Sprint-1 and Sprint-2 are already created on the Board, next one should be Sprint-3
        // User should not be able to create a new Sprint for a Board that is archived



        // ========== StartNextSprint ==========
        // User should be able to start next Sprint only if no active Sprints



        // ========== EndActiveSprint ==========
        // User should be able to end active Sprint



        // ========== ArchiveBoard ==========
        // User should be able to archive the Board only if no active Sprints
    }
}
