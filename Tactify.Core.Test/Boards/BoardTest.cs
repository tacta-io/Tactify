﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tactify.Core.Boards;
using Tactify.Core.Boards.DomainEvents;

namespace Tactify.Core.Test.Boards
{
    [TestClass]
    public class BoardTest
    {
        // 1. A new Board should be created if user provides valid board identifier and board description
        [TestMethod]        
        public void Test1()
        {
            // Given            
            var boardIdentifier = "COB";
            var description = "Constructiv Benefits";

            // When
            var board = Board.OpenBoard(boardIdentifier, description);

            // Then
            var @event = board.DomainEvents.Single() as BoardOpened;

            Assert.IsNotNull(@event);
            Assert.AreEqual("Board-COB", @event.AggregateId);
            Assert.AreEqual(description, @event.Description);
        }

        // 2. User should not be able to create a new Board if description is not provided
        [TestMethod]
        public void Test2()
        {
            // Given            
            var boardIdentifier = "COB";
            var description = string.Empty;

            // When // Then
            Assert.ThrowsException<Exception>(() => Board.OpenBoard(boardIdentifier, description));
        }

        // 3. User should not be able to create a new Board if board identifier is not provided
        [TestMethod]
        public void Test3()
        {
            // Given            
            var boardIdentifier = "";
            var description = "Constructiv Benefits";

            // When // Then
            Assert.ThrowsException<Exception>(() => Board.OpenBoard(boardIdentifier, description));
        }

        // 4. User should be able to create a new Sprint for a Board, and Sprint number should start from 1
        // 5. If Sprint-1 and Sprint-2 are already created on the Board, next one should be Sprint-3
        // 6. User should not be able to create a new Sprint for a Board that is archived
        // 7. User should be able to start next Sprint if no active Sprints
    }
}
