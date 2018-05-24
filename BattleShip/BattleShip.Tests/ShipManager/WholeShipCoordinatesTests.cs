using System;
using System.CodeDom;
using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.PositionParserTests
{
    [TestClass]
    public class WholeShipCoordinatesTests
    {
        [TestMethod]
        public void HorizontalOutputCorrectly()
        {
           
            ShipManager shipManager = new ShipManager();

            int startX = 0;
            int startY = 0;
            int shipSize = 5;
            Direction direction = Direction.Horizontal;


            List<Position> output = shipManager.WholeShipPositions(new Position(0, 0), shipSize, direction);

            output[0].X.Should().Be(0);
            output[0].Y.Should().Be(0);

            output[1].X.Should().Be(1);
            output[1].Y.Should().Be(0);

            output[2].X.Should().Be(2);
            output[2].Y.Should().Be(0);

            output[3].X.Should().Be(3);
            output[3].Y.Should().Be(0);

            output[4].X.Should().Be(4);
            output[4].Y.Should().Be(0);

        }

        [TestMethod]
        public void VerticalOutputCorrectly()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 0;
            int startY = 0;
            int shipSize = 5;
            Direction direction = Direction.Vertical;

            List<Position> output = shipManager.WholeShipPositions(new Position(0,0), shipSize, direction);

            output[0].X.Should().Be(0);
            output[0].Y.Should().Be(0);

            output[1].X.Should().Be(0);
            output[1].Y.Should().Be(1);

            output[2].X.Should().Be(0);
            output[2].Y.Should().Be(2);

            output[3].X.Should().Be(0);
            output[3].Y.Should().Be(3);

            output[4].X.Should().Be(0);
            output[4].Y.Should().Be(4);

        }

        [TestMethod]
        public void HorizontalSmallShipOutputCorrectly()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 0;
            int startY = 0;
            int shipSize = 3;
            Direction direction = Direction.Horizontal;

            List<Position> output = shipManager.WholeShipPositions(new Position(0, 0), shipSize, direction);

            output[0].X.Should().Be(0);
            output[0].Y.Should().Be(0);

            output[1].X.Should().Be(1);
            output[1].Y.Should().Be(0);

            output[2].X.Should().Be(2);
            output[2].Y.Should().Be(0);
        }

        [TestMethod]
        public void VerticalSmallShipOutputCorrectly()
        {
            ShipManager shipManager = new ShipManager();


            int startX = 0;
            int startY = 0;
            int shipSize = 3;
            Direction direction = Direction.Vertical;

            List<Position> output = shipManager.WholeShipPositions(new Position(0, 0), shipSize, direction);

            output[0].X.Should().Be(0);
            output[0].Y.Should().Be(0);

            output[1].X.Should().Be(0);
            output[1].Y.Should().Be(1);

            output[2].X.Should().Be(0);
            output[2].Y.Should().Be(2);
        }

        [TestMethod]
        public void HorizontalOnePositionShipOutputCorrectly()
        {
            ShipManager shipManager = new ShipManager();


            int startX = 6;
            int startY = 7;
            int shipSize = 1;
            Direction direction = Direction.Horizontal;

            List<Position> output = shipManager.WholeShipPositions(new Position(6, 7), shipSize, direction);

            output[0].X.Should().Be(6);
            output[0].Y.Should().Be(7);
        }

        [TestMethod]
        public void HorizontalOnePositionShipNotOutOfRange()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 6;
            int startY = 7;
            int shipSize = 1;
            Direction direction = Direction.Horizontal;

            List<Position> output = shipManager.WholeShipPositions(new Position(7, 7), shipSize, direction);

            output[0].X.Should().Be(7);
            output[0].Y.Should().Be(7);
        }

        [TestMethod]
        public void VerticalOnePositionShipOutputCorrectly()
        {
            ShipManager shipManager = new ShipManager();


            int startX = 6;
            int startY = 7;
            int shipSize = 1;
            Direction direction = Direction.Vertical;

            List<Position> output = shipManager.WholeShipPositions(new Position(6, 7), shipSize, direction);

            output[0].X.Should().Be(6);
            output[0].Y.Should().Be(7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VerticalOnePositionShipNotOutOfRange()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 6;
            int startY = 7;
            int shipSize = 1;
            Direction direction = Direction.Horizontal;

            List<Position> output = shipManager.WholeShipPositions(new Position(6, 7), shipSize, direction);

            output[1].X.Should().Be(6);
            output[1].Y.Should().Be(8);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void VerticalNotOutOfRangePlacePosition()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 0;
            int startY = 0;
            int shipSize = 5;
            Direction direction = Direction.Vertical;

            List<Position> output = shipManager.WholeShipPositions(new Position(0, 0), shipSize, direction);

            output[5].X.Should().Be(0);
            output[5].Y.Should().Be(5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HorizontalNotOutOfRangePlacePosition()
        {
            ShipManager shipManager = new ShipManager();

            int startX = 0;
            int startY = 0;
            int shipSize = 5;
            Direction direction = Direction.Horizontal;

            List<Position> output = shipManager.WholeShipPositions(new Position(0, 0), shipSize, direction);

            output[5].X.Should().Be(5);
            output[5].Y.Should().Be(0);
        }
    }
}
