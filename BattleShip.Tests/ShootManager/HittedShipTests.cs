using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Implementations.Tests
{
    [TestClass]
    public class HitShipIndexTests
    {
        [TestMethod]
        public void HurtAircraft()
        {
            IShootManager shootManager = new ShootManager();

            var ships = InitializeShips();

            var hitShipPosition = new Position(0,0);

            Ship theShip= shootManager.HittedShip(hitShipPosition, ships);
            var result = theShip.ShipType;

            result.Should().Be(ShipType.AircraftCarrier);

        }


        [TestMethod]
        public void HurtBattleShip()
        {
            IShootManager shootManager = new ShootManager();

            var ships = InitializeShips();

            var hitShipPosition = new Position(1, 0);

            var theShip = shootManager.HittedShip(hitShipPosition, ships);
            var result = theShip.ShipType;

            result.Should().Be(ShipType.BattleShip);
        }

        [TestMethod]
        public void HurtCruiser()
        {
            IShootManager shootManager = new ShootManager();

            var ships = InitializeShips();

            var hitShipPosition = new Position(2, 1);

            var theShip = shootManager.HittedShip(hitShipPosition, ships);
            var result = theShip.ShipType;

            result.Should().Be(ShipType.Cruiser);
        }

        [TestMethod]
        public void InWater()
        {
            IShootManager shootManager = new ShootManager();

            var ships = InitializeShips();

            var hitShipPosition = new Position(9, 9);

            var result = shootManager.HittedShip(hitShipPosition, ships);


            result.Should().Be(null);
        }


        private static List<Ship> InitializeShips()
        {
            List<Ship> ships = new List<Ship>();

            //prepare List<HitComputerShip>
            Ship s1 = new Ship(ShipType.AircraftCarrier, 5);
            List<Position> s1Positions = new List<Position>();
            Position p1 = new Position(0, 0);
            Position p2 = new Position(0, 1);
            Position p3 = new Position(0, 2);
            Position p4 = new Position(0, 3);
            Position p5 = new Position(0, 4);
            s1Positions.Add(p1);
            s1Positions.Add(p2);
            s1Positions.Add(p3);
            s1Positions.Add(p4);
            s1Positions.Add(p5);
            s1.Positions.AddRange(s1Positions);

            Ship s2 = new Ship(ShipType.BattleShip, 4);
            List<Position> s2Positions = new List<Position>();
            Position p6 = new Position(1, 0);
            Position p7 = new Position(1, 1);
            Position p8 = new Position(1, 2);
            Position p9 = new Position(1, 3);
            s2Positions.Add(p6);
            s2Positions.Add(p7);
            s2Positions.Add(p8);
            s2Positions.Add(p9);
            s2.Positions.AddRange(s2Positions);

            Ship s3 = new Ship(ShipType.Cruiser, 3);
            List<Position> s3Positions = new List<Position>();
            Position p10 = new Position(2, 0);
            Position p11 = new Position(2, 1);
            Position p12 = new Position(2, 2);
            s3Positions.Add(p10);
            s3Positions.Add(p11);
            s3Positions.Add(p12);
            s3.Positions.AddRange(s3Positions);

            ships.Add(s1);
            ships.Add(s2);
            ships.Add(s3);

            return ships;
        }

    }

    



}