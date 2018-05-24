using System;
using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.ErrorManagerTests
{


    [TestClass]
    public class IsValidPositionTests
    {
        [TestMethod]
        public void OnePositionOverRange()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(3, 11);
            var columnSize = 10;
            var rowSize = 10;

            bool output = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            output.Should().Be(false);
        }

        [TestMethod]
        public void NotOverRange()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(9, 0);
            var columnSize = 10;
            var rowSize = 10;

            bool output = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            output.Should().Be(true);
        }

        [TestMethod]
        public void Overlap()
        {
            var positionValidator = new PositionValidator();

            var ships = InitializeShips();

            var userPosition = new Position(5,10);
            var columnSize = 10;
            var rowSize = 10;

            bool output = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            output.Should().Be(false);
        }

        [TestMethod]
        public void NotOverlap()
        {
            var positionValidator = new PositionValidator();

            var ships = InitializeShips();

            var userPosition = new Position(0, 6);
            var columnSize = 10;
            var rowSize = 10;

            bool output = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            output.Should().Be(true);
        }

        [TestMethod]
        public void NegativeYResturnsfalse()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(0, -1);
            var columnSize = 10;
            var rowSize = 10;
           

            var actual = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);
            actual.Should().Be(false);
        }

        [TestMethod]
        public void NegativeXResturnsfalse()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(-2, 3);
            var columnSize = 10;
            var rowSize = 10;

            var actual = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            actual.Should().Be(false);
        }

        [TestMethod]
        public void OutOfPlayFieldResturnsFalse()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(0, 10);
            var columnSize = 10;
            var rowSize = 10;

            var actual = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);

            actual.Should().Be(false);
        }

        [TestMethod]
        public void ElevenResturnsFalse()
        {
            var positionValidator = new PositionValidator();
            var ships = InitializeShips();

            var userPosition = new Position(-16, 10);
            var columnSize = 10;
            var rowSize = 10;

            var actual = positionValidator.IsValidPosition(userPosition, columnSize, rowSize, ships);


            actual.Should().Be(false);
        }


        private static List<Ship> InitializeShips()
        {
            var ships = new List<Ship>();

            ships.Add(new Ship(ShipType.AircraftCarrier, 5));
            ships.Add(new Ship(ShipType.BattleShip, 4));
            ships.Add(new Ship(ShipType.Cruiser, 3));
            ships.Add(new Ship(ShipType.Destroyer, 2));
            ships.Add(new Ship(ShipType.Destroyer, 2));
            ships.Add(new Ship(ShipType.Submarine, 1));
            ships.Add(new Ship(ShipType.Submarine, 1));

            var positionAir = new List<Position>();
            var positionCruiser = new List<Position>();
            var positionDestroyer1 = new List<Position>();
            var positionSubmarine1 = new List<Position>();

            var p1 = new Position(5, 10);
            var p2 = new Position(5, 11);
            var p3 = new Position(5, 12);
            var p4 = new Position(5, 13);
            var p5 = new Position(5, 14);
            var p6 = new Position(5, 15);

            positionAir.Add(p1);
            positionAir.Add(p2);
            positionAir.Add(p3);
            positionAir.Add(p4);
            positionAir.Add(p5);
            positionAir.Add(p6);

            ships[0].Positions.AddRange(positionAir);
            //ships[1].Positions.AddRange(positionDestroyer1);
            //ships[2].Positions.AddRange(positionSubmarine1);


            return ships;
        }



        //    [TestMethod]
        //    public void OnePointOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(11, 11);
        //        wantPositions.Add(wantP1);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }

        //    [TestMethod]
        //    public void AllShipNotOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(0, 0);
        //        Position wantP2 = new Position(0, 1);
        //        Position wantP3 = new Position(0, 2);
        //        Position wantP4 = new Position(0, 3);
        //        Position wantP5 = new Position(0, 4);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(false);
        //    }


        //    [TestMethod]
        //    public void AllShipOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(10, 0);
        //        Position wantP2 = new Position(10, 1);
        //        Position wantP3 = new Position(10, 2);
        //        Position wantP4 = new Position(10, 3);
        //        Position wantP5 = new Position(10, 4);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }

        //    [TestMethod]
        //    public void OnePointOfShipOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(4, 6);
        //        Position wantP2 = new Position(4, 7);
        //        Position wantP3 = new Position(4, 8);
        //        Position wantP4 = new Position(4, 9);
        //        Position wantP5 = new Position(4, 10);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }

        //    [TestMethod]
        //    public void APartOfShipOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(4, 7);
        //        Position wantP2 = new Position(4, 8);
        //        Position wantP3 = new Position(4, 9);
        //        Position wantP4 = new Position(4, 10);
        //        Position wantP5 = new Position(4, 11);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }

        //    [TestMethod]
        //    public void EastSouthNotOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(5, 8);
        //        Position wantP2 = new Position(6, 8);
        //        Position wantP3 = new Position(7, 8);
        //        Position wantP4 = new Position(8, 8);
        //        Position wantP5 = new Position(9, 8);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(false);
        //    }

        //    [TestMethod]
        //    public void EastSouthOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(7, 8);
        //        Position wantP2 = new Position(8, 8);
        //        Position wantP3 = new Position(9, 8);
        //        Position wantP4 = new Position(10, 8);
        //        Position wantP5 = new Position(11, 8);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }

        //    [TestMethod]
        //    public void EastNorthNotOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(2, 5);
        //        Position wantP2 = new Position(2, 6);
        //        Position wantP3 = new Position(2, 7);
        //        Position wantP4 = new Position(2, 8);
        //        Position wantP5 = new Position(2, 9);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(false);
        //    }

        //    [TestMethod]
        //    public void EastNorthOverEdage()
        //    {
        //        PositionValidator positionValidator = new PositionValidator();

        //        List<Position> wantPositions = new List<Position>();
        //        Position wantP1 = new Position(2, 6);
        //        Position wantP2 = new Position(2, 7);
        //        Position wantP3 = new Position(2, 8);
        //        Position wantP4 = new Position(2, 9);
        //        Position wantP5 = new Position(2, 10);
        //        wantPositions.Add(wantP1);
        //        wantPositions.Add(wantP2);
        //        wantPositions.Add(wantP3);
        //        wantPositions.Add(wantP4);
        //        wantPositions.Add(wantP5);

        //        bool output = positionValidator.IsOverRange(wantPositions);

        //        output.Should().Be(true);
        //    }
        //}

    }

}
