using System;
using System.Collections.Generic;
using BattleShip.DataContracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.ShootManager
{
    [TestClass]
    public class IsAllShipsSunkenTests
    {
        [TestMethod]
        public void NotSunken()
        {
            var shootManager = new Implementations.ShootManager();

            var ships = InitializeShips();

            var result = shootManager.IsAllShipsSunken(ships);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void OneShipSunken()
        {
            var shootManager = new Implementations.ShootManager();

            var ships = InitializeShips();

            // Destroyer Sunken
            var shoot1 = new Position(5, 1);
            var shoot2 = new Position(5, 2);
            shootManager.RemoveHitPosition(shoot1, ships);
            shootManager.RemoveHitPosition(shoot2, ships);

            var result = shootManager.IsAllShipsSunken(ships);

            result.Should().BeFalse();
        }

        [TestMethod]
        public void TwoShipsSunken()
        {
            var shootManager = new Implementations.ShootManager();

            var ships = InitializeShips();

            // Destroyer Sunken
            var shoot1 = new Position(5, 1);
            var shoot2 = new Position(5, 2);
            shootManager.RemoveHitPosition(shoot1, ships);
            shootManager.RemoveHitPosition(shoot2, ships);
            // Submarine Sunken
            var shoot3 = new Position(8, 8);
            shootManager.RemoveHitPosition(shoot3, ships);

            var result = shootManager.IsAllShipsSunken(ships);

            result.Should().BeFalse();
        }


        [TestMethod]
        public void AllSunken()
        {
            var shootManager = new Implementations.ShootManager();

            var ships = InitializeShips();

            // Cruiser Sunken
            var shoot1 = new Position(2, 0);
            var shoot2 = new Position(2, 1);
            var shoot3 = new Position(2, 2);
            shootManager.RemoveHitPosition(shoot1,ships);
            shootManager.RemoveHitPosition(shoot2,ships);
            shootManager.RemoveHitPosition(shoot3,ships);
            // Destroyer Sunken
            var shoot4 = new Position(5, 1);
            var shoot5 = new Position(5, 2);
            shootManager.RemoveHitPosition(shoot4, ships);
            shootManager.RemoveHitPosition(shoot5, ships);
            // Submarine Sunken
            var shoot6 = new Position(8, 8);
            shootManager.RemoveHitPosition(shoot6, ships);

            var result = shootManager.IsAllShipsSunken(ships);

            result.Should().BeTrue();
        }


        private static List<Ship> InitializeShips()
        {
            List<Ship> ships = new List<Ship>();

            //prepare List<HitComputerShip>

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
            var p13 = new Position(5, 1);
            var p14 = new Position(5, 2);
            s4Positions.Add(p13);
            s4Positions.Add(p14);
            s4.Positions.AddRange(s4Positions);


            var s5 = new Ship(ShipType.Submarine, 1);
            var s5Positions = new List<Position>();
            var p16 = new Position(8, 8);
            s5Positions.Add(p16);
            s5.Positions.AddRange(s5Positions);


            ships.Add(s3);
            ships.Add(s4);
            ships.Add(s5);

            return ships;
        }

    }
}
