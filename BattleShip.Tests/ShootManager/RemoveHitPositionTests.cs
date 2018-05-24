using System.Collections.Generic;
using BattleShip.DataContracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.ShootManager
{
    [TestClass]
    public class RemoveHitPositionTests
    {
        [TestMethod]
        public void Shoot00()
        {
            var shootManager = new Implementations.ShootManager();

            List<Ship> ships = AListShips();

            var iShoot = new Position(0, 0);

            shootManager.RemoveHitPosition(iShoot, ships);

            ships[0].Positions[0].X.Should().Be(0);
            ships[0].Positions[0].Y.Should().Be(1);

            bool result = ships[0].Positions.Contains(new Position(0, 0));

            result.Should().BeFalse();
        }


        [TestMethod]
        public void Shoot10()
        {
            Implementations.ShootManager shootManager = new Implementations.ShootManager();

            List<Ship> ships = AListShips();

            Position iShoot = new Position(1, 0);

            shootManager.RemoveHitPosition(iShoot, ships);

            ships[1].Positions[0].X.Should().Be(1);
            ships[1].Positions[0].Y.Should().Be(1);
        }

        [TestMethod]
        public void Shoot21()
        {
            Implementations.ShootManager shootManager = new Implementations.ShootManager();

            List<Ship> ships = AListShips();

            Position iShoot1 = new Position(2, 0);
            Position iShoot2 = new Position(2, 1);
            Position iShoot3 = new Position(2, 2);

            shootManager.RemoveHitPosition(iShoot1, ships);
            shootManager.RemoveHitPosition(iShoot2, ships);
            shootManager.RemoveHitPosition(iShoot3, ships);

            ships[2].Positions.Count.Should().Be(0);
        }


        private static List<Ship> AListShips()
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