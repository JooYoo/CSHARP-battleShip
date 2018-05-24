using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using FluentAssertions;

namespace BattleShip.Implementations.Tests
{
    [TestClass]
    public class DisplayBattleTableTests
    {
        [TestMethod]
        public void AllShipsAreThere()
        {
            

            List<Ship> playerShips = InitializePlayerShips();
            List<Ship> computerShips = InitializePcShips();


            string result = GraphicManager.DisplayBattleTables(playerShips, computerShips);

            bool isExist1 = result.Contains("AircraftCarrier");
            bool isExist2 = result.Contains("BattleShip");
            bool isExist3 = result.Contains("Cruiser");


            isExist1.Should().BeTrue();
            isExist2.Should().BeTrue();
            isExist3.Should().BeTrue();

        }

        [TestMethod]
        public void ComputerCruiserHasSunken()
        {

            List<Ship> playerShips = InitializePlayerShips();
            List<Ship> computerShips = InitializePcShips();

            // PC Cruiser Sunken
            ShootManager shootManager = new ShootManager();
            shootManager.RemoveHitPosition(new Position(2, 0), computerShips);
            shootManager.RemoveHitPosition(new Position(2, 1), computerShips);
            shootManager.RemoveHitPosition(new Position(2, 2), computerShips);

            string result = GraphicManager.DisplayBattleTables(playerShips, computerShips);

            bool isExist3 = result.Contains("Cruiser");

            isExist3.Should().BeTrue();
        }

        [TestMethod]
        public void AllCruiserHasSunken()
        {
            List<Ship> playerShips = InitializePlayerShips();
            List<Ship> computerShips = InitializePcShips();

            // PC Cruiser Sunken
            ShootManager shootManager = new ShootManager();
            shootManager.RemoveHitPosition(new Position(2, 0), computerShips);
            shootManager.RemoveHitPosition(new Position(2, 1), computerShips);
            shootManager.RemoveHitPosition(new Position(2, 2), computerShips);
            // Player Cruiser Sunken
            shootManager.RemoveHitPosition(new Position(3, 0), playerShips);
            shootManager.RemoveHitPosition(new Position(3, 1), playerShips);
            shootManager.RemoveHitPosition(new Position(3, 2), playerShips);

            string result = GraphicManager.DisplayBattleTables(playerShips, computerShips);

            bool isExist = result.Contains("Cruiser");

            isExist.Should().BeFalse();
        }


        private static List<Ship> InitializePlayerShips()
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
            Position p10 = new Position(3, 0);
            Position p11 = new Position(3, 1);
            Position p12 = new Position(3, 2);
            s3Positions.Add(p10);
            s3Positions.Add(p11);
            s3Positions.Add(p12);
            s3.Positions.AddRange(s3Positions);

            ships.Add(s1);
            ships.Add(s2);
            ships.Add(s3);

            return ships;
        }

        private static List<Ship> InitializePcShips()
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