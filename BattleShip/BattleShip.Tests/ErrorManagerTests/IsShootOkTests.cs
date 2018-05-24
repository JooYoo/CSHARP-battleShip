using System;
using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.ErrorManagerTests
{
    [TestClass]
    public class IsShootOkTests
    {
        [TestMethod]
        public void OutOfBoundsY()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(0, 100000000);
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();

        }

        [TestMethod]
        public void OutOfBoundsX()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(100000000, 0);
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();

        }

        [TestMethod]
        public void CorrectlyPosition()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            player.Positions= new List<Position>();
            player.Hits = new List<Hit>();
            var iShootPosition = new Position(1, 2);

            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeTrue();

        }

        [TestMethod]
        public void NegativeXPosition()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(-2, 3);

            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();

        }


        [TestMethod]
        public void NegativeYPosition()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(0, -1);
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();

        }

        [TestMethod]
        public void OutOfBoundX()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(11, 0);
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void OutOfBoundY()
        {
            var positionValidator = new PositionValidator();

            var player = new Player();
            var iShootPosition = new Position(0, 12);
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(iShootPosition, columnSize, rowSize, player);

            result.Should().BeFalse();
        }


        // When User 'Enter' Typo
        [TestMethod]
        public void NulliShootPosition()
        {
            var positionValidator = new PositionValidator();
            var player = new Player();
            var columnSize = 10;
            var rowSize = 10;

            bool result = positionValidator.IsShootOk(null, columnSize, rowSize, player);

            result.Should().BeFalse();
        }



    }
}
