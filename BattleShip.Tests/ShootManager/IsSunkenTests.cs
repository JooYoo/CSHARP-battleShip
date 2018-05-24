using System;
using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Implementations.Tests
{
    [TestClass]
    public class IsSunkenTests
    {
        [TestMethod]
        public void NotSunken()
        {
            var shootManager = new ShootManager();

            int shipIndex = 0;
            List<Ship> ships = InitializeShips();

            bool result = shootManager.IsSunken(ships[0]);

            result.Should().BeFalse();
        }


        [TestMethod]
        public void OnePositionHit()
        {
            var shootManager = new ShootManager();

            int shipIndex = 0;
            List<Ship> ships = InitializeShips();

            //be hited once
            Position iShootPosition = new Position(0, 1);
            shootManager.RemoveHitPosition(iShootPosition, ships);

            bool result = shootManager.IsSunken(ships[0]);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoPositionsHit()
        {
            var shootManager = new ShootManager();

            int shipIndex = 0;
            List<Ship> ships = InitializeShips();

            //be hited twice
            Position iShootPosition1 = new Position(0, 1);
            Position iShootPosition2 = new Position(0, 2);
            shootManager.RemoveHitPosition(iShootPosition1, ships);
            shootManager.RemoveHitPosition(iShootPosition2, ships);

            bool result = shootManager.IsSunken(ships[0]);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void ThreePositionsHit()
        {
            var shootManager = new ShootManager();

            int shipIndex = 0;
            List<Ship> ships = InitializeShips();

            //be hited twice
            Position iShootPosition1 = new Position(0, 1);
            Position iShootPosition2 = new Position(0, 2);
            Position iShootPosition3 = new Position(0, 3);
            shootManager.RemoveHitPosition(iShootPosition1, ships);
            shootManager.RemoveHitPosition(iShootPosition2, ships);
            shootManager.RemoveHitPosition(iShootPosition3, ships);

            bool result = shootManager.IsSunken(ships[0]);

            result.Should().BeFalse();
        }


        [TestMethod]
        public void AllPositionsHit()
        {
            var shootManager = new ShootManager();

            int shipIndex = 0;
            List<Ship> ships = InitializeShips();

            //be hited twice
            Position iShootPosition1 = new Position(0, 0);
            Position iShootPosition2 = new Position(0, 1);
            Position iShootPosition3 = new Position(0, 2);
            Position iShootPosition4 = new Position(0, 3);
            Position iShootPosition5 = new Position(0, 4);
            shootManager.RemoveHitPosition(iShootPosition1, ships);
            shootManager.RemoveHitPosition(iShootPosition2, ships);
            shootManager.RemoveHitPosition(iShootPosition3, ships);
            shootManager.RemoveHitPosition(iShootPosition4, ships);
            shootManager.RemoveHitPosition(iShootPosition5, ships);

            bool result = shootManager.IsSunken(ships[0]);

            result.Should().BeTrue();
        }

        [TestMethod]
        public void SubmarineSunken()
        {
            var shootManager = new ShootManager();

            int shipIndex = 4;
            List<Ship> ships = InitializeShips();

            //be hited twice
            Position iShootPosition1 = new Position(8, 8);
            shootManager.RemoveHitPosition(iShootPosition1, ships);

            bool result = shootManager.IsSunken(ships[4]);

            result.Should().BeTrue();
        }


        private static List<Ship> InitializeShips()
        {
            List<Ship> ships = new List<Ship>();

            //prepare List<HitComputerShip>
            var s1 = new Ship(ShipType.AircraftCarrier, 5);
            var s1Positions = new List<Position>();
            var p1 = new Position(0, 0);
            var p2 = new Position(0, 1);
            var p3 = new Position(0, 2);
            var p4 = new Position(0, 3);
            var p5 = new Position(0, 4);
            s1Positions.Add(p1);
            s1Positions.Add(p2);
            s1Positions.Add(p3);
            s1Positions.Add(p4);
            s1Positions.Add(p5);
            s1.Positions.AddRange(s1Positions);

            var s2 = new Ship(ShipType.BattleShip, 4);
            var s2Positions = new List<Position>();
            var p6 = new Position(1, 0);
            var p7 = new Position(1, 1);
            var p8 = new Position(1, 2);
            var p9 = new Position(1, 3);
            s2Positions.Add(p6);
            s2Positions.Add(p7);
            s2Positions.Add(p8);
            s2Positions.Add(p9);
            s2.Positions.AddRange(s2Positions);

            var s3 = new Ship(ShipType.Cruiser, 3);
            var s3Positions = new List<Position>();
            var p10 = new Position(2, 0);
            var p11 = new Position(2, 1);
            var p12 = new Position(2, 2);
            s3Positions.Add(p10);
            s3Positions.Add(p11);
            s3Positions.Add(p12);
            s3.Positions.AddRange(s3Positions);

            var s4 = new Ship(ShipType.Destroyer, 2);
            var s4Positions = new List<Position>();
            var p13 = new Position(5,1);
            var p14 = new Position(5,2);
            s4Positions.Add(p13);
            s4Positions.Add(p14);
            s4.Positions.AddRange(s4Positions);


            var s5 = new Ship(ShipType.Submarine, 1);
            var s5Positions = new List<Position>();
            var p16 = new Position(8,8);
            s5Positions.Add(p16);
            s5.Positions.AddRange(s5Positions);

            ships.Add(s1);
            ships.Add(s2);
            ships.Add(s3);
            ships.Add(s4);
            ships.Add(s5);

            return ships;
        }
    }
}
