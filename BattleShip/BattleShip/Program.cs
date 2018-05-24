using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using BattleShip.OperationContracts;
using WMPLib;

namespace BattleShip
{
    class Program
    {
        // initialize Column and Row size
        //private const int ColumnSize = 10;
        //private const int RowSize = 10;

        private static IPositionParser positionParser;
        private static IPositionValidator positionValidator;
        private static IRandomManager randomManager;
        private static IShipManager shipManager;
        private static IShootManager shootManager;
        private static IPlaceManager placeManager;

        public static void Main()
        {
            #region [on] 0. Instance
            var battlefield = new Battlefield(10, 10);
            positionParser = new PositionParser();
            positionValidator = new PositionValidator();
            randomManager = new RandomManager();
            shipManager = new ShipManager();
            shootManager = new ShootManager();
            placeManager = new PlaceManager();
            var battleBgm = new WindowsMediaPlayer();
            var winnerBgm = new WindowsMediaPlayer();

            #endregion


            #region[off] 1. Welcome Screen
            //GraphicManager.WelcomeScreen();
            #endregion


            #region [on] 1.1. BGM

            SoundEffects.BattleBgmPlayer(battleBgm);

            #endregion


            #region[on] 2. Ships Topf

            // instance two players
            var player = new Player();
            player.Positions = new List<Position>();
            player.Hits = new List<Hit>();
            player.Ships = new List<Ship>();

            var computer = new Player();
            computer.Positions = new List<Position>();
            computer.Hits = new List<Hit>();
            computer.Ships = new List<Ship>();

            #endregion


            #region[on] 3. Place PlayerShips and ComputerShips

            // create player Ships
            player.Ships = InitializeShips();
            // place player ships
            // [!!]  placeManager.PlacePlayerShips(player.Ships, positionParser, positionValidator, shipManager,battlefield);  
            placeManager.PlaceComputerShips(player.Ships, randomManager, shipManager, positionValidator, battlefield);

            // create ships for pc
            computer.Ships = InitializeShips();
            // place pc ships
            placeManager.PlaceComputerShips(computer.Ships, randomManager, shipManager, positionValidator, battlefield);

            // Add positions into Instances
            foreach (var playerShip in player.Ships)
                player.Positions.AddRange(playerShip.Positions);
            var allPlayerShipsPositions = player.Positions;

            foreach (var computerShip in computer.Ships)
                computer.Positions.AddRange(computerShip.Positions);
            var allComputerShipsPositions = computer.Positions;

            #endregion


            #region+ [on] display pcShips for Testing
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("        God Mode");
            Console.WriteLine("        PC Ships:");
            Console.WriteLine();
            Console.WriteLine(GraphicManager.DisplayBattlefield(battlefield.ColumnSize, battlefield.RowSize, computer.Ships).PadRight(20));
            Console.ReadKey();
            SoundEffects.SetShipSoundPlayer();
            #endregion


            #region  + [off] display Win view for Testing

            //battleBgm.close();
            //EndGameManager.WhoWin(player, computer, winnerBgm, shootManager);
            //EndGameManager.RestartGame(winnerBgm);

            #endregion


            #region[on] 4. Game begin view

            GraphicManager.GameStartView(player, computer, battlefield);

            #endregion


            #region[on] 5. Player || PC shots
            do // End the Game
            {
                do  // Player == * ==> PC 
                {
                    var iShootPosition = ShootManager.PlayerShoot(player, computer, battlefield, positionParser, positionValidator);

                    if (allComputerShipsPositions.Contains(iShootPosition)) // Hit Ship
                    {
                        computer.Hits.Add(new Hit(HitType.Ship, iShootPosition));
                        shootManager.HitShip(player, computer, iShootPosition, computer.Ships, battlefield);
                    }
                    else // Hit Water
                    {
                        computer.Hits.Add(new Hit(HitType.Water, iShootPosition));
                        shootManager.HitWater(player, computer, iShootPosition, battlefield, positionParser);
                    }

                    // Is all Player Ships Sunken
                    if (shootManager.IsAllShipsSunken(computer.Ships)) break;

                } while (computer.Hits.Last().HitType == HitType.Ship); // one more time shoot


                do // PC == * ==> Player
                {
                    // if All player ships have Sunked skip all those code
                    if (shootManager.IsAllShipsSunken(computer.Ships)) break;

                    var pcShootPosition = ShootManager.ComputerShoot(player, computer, battlefield, randomManager, positionParser);

                    if (allPlayerShipsPositions.Contains(pcShootPosition)) // Hit Ship
                    {
                        player.Hits.Add(new Hit(HitType.Ship, pcShootPosition));
                        shootManager.HitShip(player, computer, pcShootPosition, player.Ships, battlefield);
                    }
                    else //Hit Water
                    {
                        player.Hits.Add(new Hit(HitType.Water, pcShootPosition));
                        shootManager.HitWater(player, computer, pcShootPosition, battlefield, positionParser);
                    }

                    // Check if all Player Ship Sunken
                    if (shootManager.IsAllShipsSunken(player.Ships)) break;

                } while (player.Hits.Last().HitType == HitType.Ship); // one more time shoot

            } while (shootManager.IsAllShipsSunken(computer.Ships) == false && shootManager.IsAllShipsSunken(player.Ships) == false); // End the game

            #endregion


            #region [on] 6. Game End View

            battleBgm.close();
            EndGameManager.WhoWin(player, computer, winnerBgm, shootManager);
            EndGameManager.RestartGame(winnerBgm);

            #endregion

           
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


            return ships;
        }


        //private static void OldMain()

        //{

        //    // Review: brauchst du die regions hier in der Methode wirklich? Was bringen die für einen Mehrwert?


        //    // Ausgabe von Spielfeld und Platzierungstabelle


        //    // User platziert Schiffe


        //    // Computer platziert Schiffe


        //    #region 0. Implementations instance

        //    //PositionParser positionParser = new PositionParser();

        //    //PositionValidator positionValidator = new PositionValidator();

        //    //RandomManager randomManager = new RandomManager();


        //    positionParser = new PositionParser();

        //    positionValidator = new PositionValidator();

        //    randomManager = new RandomManager();

        //    shootManager = new ShootManager();


        //    #endregion


        //    #region 0.1. HitComputerShip Topf


        //    // the Enermy PlayField

        //    List<Position> hitSuccessPositions = new List<Position>();

        //    List<Position> hitFailPositions = new List<Position>();


        //    List<HitComputerShip> myShips = new List<HitComputerShip>();

        //    List<HitComputerShip> pcShips = new List<HitComputerShip>();


        //    #endregion


        //    #region[off] 1. Display Welcome Screen


        //    //GraphicManager.WelcomeScreen();


        //    #endregion


        //    #region[on] 2. Place User Ships

        //    // Review: warum gibt es ein Destroyer A und Destroyer B? Warum gibt es zwei Submarine A?


        //    myShips.Add(new HitComputerShip(ShipType.AircraftCarrier, 5));

        //    myShips.Add(new HitComputerShip(ShipType.BattleShip, 4));

        //    myShips.Add(new HitComputerShip(ShipType.Cruiser, 3));

        //    myShips.Add(new HitComputerShip(ShipType.Destroyer, 2));

        //    myShips.Add(new HitComputerShip(ShipType.Destroyer, 2));

        //    myShips.Add(new HitComputerShip(ShipType.Submarine, 1));

        //    myShips.Add(new HitComputerShip(ShipType.Submarine, 1));


        //    // Review: wäre hier eine Komponente nicht sinnvoller? positionParser und positionValidator sind abhängigkeiten, d.h. die könntest du im Konstruktor der Komponente reinreichen.

        //    // Review: brauchst du "allShipsCoordinates" wirklich? Die Positionen sind doch auf den Schiffen gespeichert?

        //    // Review: Wenn du dir außerdem die Schiffe in einer Liste halten würdest, könntest du hier mit foreach über die Liste iterieren.


        //    foreach (var ship in myShips)

        //    {

        //        var currentArrowPosition = myShips.IndexOf(ship);

        //        ShipPlaceHelper(allMyShipsPositions, positionParser, positionValidator, ship);

        //    }


        //    #endregion


        //    #region[on] 3. PC Ships placement


        //    // Review: wie unterscheiden sich denn die Spieler- und Computerschiffe?


        //    pcShips.Add(new HitComputerShip("Aircraft Carrier (PC)", 5));

        //    pcShips.Add(new HitComputerShip("Battle HitComputerShip (PC)", 4));

        //    pcShips.Add(new HitComputerShip("Cruiser (PC)", 3));

        //    pcShips.Add(new HitComputerShip("Destroyer A (PC)", 2));

        //    pcShips.Add(new HitComputerShip("Destroyer B (PC)", 2));

        //    pcShips.Add(new HitComputerShip("Submarine A (PC)", 1));

        //    pcShips.Add(new HitComputerShip("Submarine A (PC)", 1));


        //    foreach (var pcShip in pcShips)

        //    {

        //        ShipPlaceHelperPc(randomManager, positionParser, positionValidator, allPcShipsPositions, pcShip);

        //    }


        //    //pcShips[0].Positions.Remove(pcShips[0].Positions[0]);


        //    #endregion


        //    #region[off] 4. Game begin view


        //    //Console.Clear();


        //    //GraphicManager.DoubleBattleFieldsDisplayer(allMyShipsPositions, hitSuccessPositions, hitFailPositions);

        //    //// HitComputerShip State Table area

        //    //GraphicManager.DisplayBattleTables();

        //    //Console.WriteLine();

        //    //// information area

        //    //Console.WriteLine("                                               ready to START");

        //    //Console.Write("                                          Press Enter to continue >");


        //    //Console.ReadKey();


        //    #endregion


        //    #region  5. Player shots

        //    Console.Clear();


        //    // 1st shoot

        //    // cheat

        //    GraphicManager.AllShipsDisplayer(allPcShipsPositions);


        //    // Display PlayField

        //    GraphicManager.DoubleBattleFieldsDisplayer(allMyShipsPositions, hitSuccessPositions, hitFailPositions);

        //    // HitComputerShip State Table area

        //    List<int> myShipsAmount = shootManager.ShipAmountList(myShips);

        //    List<int> pcShipsAmount = shootManager.ShipAmountList(pcShips);

        //    GraphicManager.DisplayBattleTables(myShipsAmount, pcShipsAmount);

        //    // information area

        //    Console.WriteLine("                                              \" You  Shoot \" ");

        //    Console.WriteLine();

        //    Console.Write("                                                POSITION >");

        //    // user input shoot position

        //    string iShoot = Console.ReadLine();

        //    //check if input legal

        //    bool isInputLegal = positionValidator.IsPositionLegal(iShoot);


        //    #region Input illegal

        //    //input illegal

        //    while (isInputLegal == false)

        //    {

        //        Console.Clear();

        //        GraphicManager.DoubleBattleFieldsDisplayer(allMyShipsPositions, hitSuccessPositions, hitFailPositions);

        //        GraphicManager.DisplayBattleTables(myShipsAmount, pcShipsAmount);

        //        Console.WriteLine("                                         ! Please input correctly ! ");

        //        Console.WriteLine("                                              \" You  Shoot \" ");

        //        Console.Write("                                                POSITION >");

        //        iShoot = Console.ReadLine();

        //        isInputLegal = positionValidator.IsPositionLegal(iShoot);

        //    }


        //    #endregion


        //    //convert to "Position"

        //    Position iShootPosition = positionParser.Parse(iShoot);

        //    if (iShootPosition == null)

        //    {

        //        // invalide eingabe

        //    }

        //    // check if hit enermy

        //    if (allPcShipsPositions.Contains(iShootPosition))

        //    {

        //        hitSuccessPositions.Add(iShootPosition); //  PlayField


        //        shootManager.RemoveHitPosition(iShootPosition, pcShips); // remove Hit Position for display

        //        allPcShipsPositions.Remove(iShootPosition);


        //        myShipsAmount = shootManager.ShipAmountList(myShips);

        //        pcShipsAmount = shootManager.ShipAmountList(pcShips);


        //        Console.Clear();

        //        GraphicManager.DoubleBattleFieldsDisplayer(allMyShipsPositions, hitSuccessPositions, hitFailPositions);

        //        GraphicManager.DisplayBattleTables(myShipsAmount, pcShipsAmount);

        //        Console.WriteLine("                                               \" HIT !!! \" ");

        //        Console.WriteLine();

        //        Console.Write("                                                Continue >");


        //        Console.ReadKey();

        //    }

        //    else

        //    {

        //        hitFailPositions.Add(iShootPosition); // for PlayFild

        //        Console.Clear();

        //        GraphicManager.DoubleBattleFieldsDisplayer(allMyShipsPositions, hitSuccessPositions, hitFailPositions);

        //        GraphicManager.DisplayBattleTables(myShipsAmount, pcShipsAmount);

        //        Console.WriteLine("                                                \" lost \" ");

        //        Console.WriteLine();

        //        Console.Write("                                                Continue >");

        //        Console.ReadKey();

        //    }


        //    // check if sunken


        //    #endregion


        //    GraphicManager.AllShipsDisplayer(allPcShipsPositions);


        //    GraphicManager.DisplayBattleTables(myShipsAmount, pcShipsAmount);

        //    Console.ReadKey();


        //    Console.Clear();

        //    Console.WriteLine("nihao");


        //    Console.ReadKey();

        //}


        //#region ShipPlaceHelperPc

        //private static void ShipPlaceHelperPc(IRandomManager randomManager, IPositionParser positionParser, IPositionValidator positionValidator, List<Position> allPcShipsCoordinates, HitComputerShip ship)

        //{

        //    // Review: helfen dir die Kommentare in den Methoden wirklich? 

        //    //1. get random start position

        //    Position position = randomManager.RandomPosition();


        //    //2. get random vertical und horizontal

        //    string direction = randomManager.RandomDirection();


        //    //3. get whole ship coordinates

        //    List<Position> wholePcShipCoordinates = positionParser.WholeShipPositions(position, ship.Size, direction);


        //    //4. check if isOverEdge or Overlap

        //    bool isOutOfEdge = positionValidator.IsOverRange(wholePcShipCoordinates);

        //    bool isOverlap = positionValidator.IsOverlap(wholePcShipCoordinates, allPcShipsCoordinates);


        //    while (isOutOfEdge || isOverlap)

        //    {

        //        position = randomManager.RandomPosition();


        //        direction = randomManager.RandomDirection();


        //        wholePcShipCoordinates = positionParser.WholeShipPositions(position, ship.Size, direction);


        //        isOutOfEdge = positionValidator.IsOverRange(wholePcShipCoordinates);

        //        isOverlap = positionValidator.IsOverlap(wholePcShipCoordinates, allPcShipsCoordinates);

        //    }


        //    //6. save PCship positions

        //    foreach (var wholePcShipCoordinate in wholePcShipCoordinates)

        //    {

        //        allPcShipsCoordinates.Add(wholePcShipCoordinate);

        //    }


        //    //7. record the HitComputerShip position information

        //    ship.Positions.AddRange(wholePcShipCoordinates);


        //}

        //#endregion


        //#region ShipPlaceHelper

        //public static void ShipPlaceHelper(List<Position> allShipsCoordinates, IPositionParser positionParser, IPositionValidator positionValidator, HitComputerShip ship )

        //{

        //    Console.Clear();


        //    // Review: helfen dir die Kommentare in den Methoden wirklich? 

        //    //display PlayField

        //    GraphicManager.AllShipsDisplayer(allShipsCoordinates);

        //    //display ShipPlaceTable

        //    GraphicManager.ShipPlaceTableDisplayer(ship.SerialNumber);

        //    // get position & check if legal

        //    var positionInput = PositionFilter(allShipsCoordinates, positionValidator, ship);


        //    //get ship Start Position

        //    Position shipPosition = positionParser.Parse(positionInput);


        //    //get ship Direction & check if legal


        //    var direction = GetCorrectDirectionFromUser(allShipsCoordinates, ship, positionInput);

        //    //get ship Coordinates

        //    List<Position> shipCoordinates = positionParser.WholeShipPositions(shipPosition, ship.Size, direction);

        //    //check if the HitComputerShip placed right

        //    shipCoordinates = OverLapOverEdgeFilter(allShipsCoordinates, positionParser, positionValidator, ship, shipCoordinates);


        //    // add Positions to AllShipsDisplayer

        //    foreach (var shipCoordinate in shipCoordinates)

        //    {

        //        allShipsCoordinates.Add(shipCoordinate);

        //    }


        //    // record Ships Positions

        //    ship.Positions.AddRange(shipCoordinates);

        //}


        //private static string PositionFilter(List<Position> allShipsCoordinates, IPositionValidator positionValidator, HitComputerShip ship)

        //{

        //    string positionInput = Console.ReadLine();

        //    bool isPositionCorrect = positionValidator.IsPositionLegal(positionInput);

        //    while (isPositionCorrect == false)

        //    {

        //        // Review: willst du das hier wirklich jedesmal machen wenn der Benutzer etwas falsches eingibt? Reicht hier nicht ein Hinweis an den Benutzer, dass die Eingabe nicht korrekt war und es wird nur das Eingabefeld neu angezeigt?

        //        Console.Clear();


        //        GraphicManager.AllShipsDisplayer(allShipsCoordinates);

        //        GraphicManager.ShipPlaceTableDisplayer(ship.SerialNumber);

        //        Console.Write(" (Please input correctly) >");

        //        positionInput = Console.ReadLine();

        //        isPositionCorrect = positionValidator.IsPositionLegal(positionInput);

        //    }

        //    return positionInput;

        //}


        //private static Direction GetCorrectDirectionFromUser(List<Position> allShipsCoordinates, HitComputerShip ship, string positionInput)

        //{

        //    var direction = DirectionFilter();

        //    while (direction == Direction.None)

        //    {


        //        Console.Clear();


        //        GraphicManager.AllShipsDisplayer(allShipsCoordinates);

        //        GraphicManager.ShipPlaceTableDisplayer(ship.SerialNumber);

        //        Console.WriteLine(positionInput);

        //        Console.Write("    Please input correctly direction >");

        //        direction = DirectionFilter();

        //    }


        //    return direction;

        //}


        //// Review: name in "Edge" geändert.
        //private static List<Position> OverLapOverEdgeFilter(List<Position> allShipsCoordinates, IPositionParser positionParser, IPositionValidator positionValidator, HitComputerShip ship, List<Position> shipCoordinates)
        //{
        //    string positionInput;
        //    Position shipPosition;
        //    Direction directionInput;
        //    bool isOut = positionValidator.IsOverRange(shipCoordinates);
        //    bool isOverlap = positionValidator.IsOverlap(shipCoordinates, allShipsCoordinates);
        //    while (isOut || isOverlap)
        //    {
        //        // Review: willst du das hier wirklich jedesmal machen wenn der Benutzer etwas falsches eingibt? Reicht hier nicht ein Hinweis an den Benutzer, dass die Eingabe nicht korrekt war und es wird nur das Eingabefeld neu angezeigt?
        //        Console.Clear();

        //        GraphicManager.AllShipsDisplayer(allShipsCoordinates);
        //        GraphicManager.ShipPlaceTableDisplayer(ship.SerialNumber);
        //        Console.Write(" (find a better place) >");
        //        positionInput = PositionFilter(allShipsCoordinates, positionValidator, ship);

        //        shipPosition = positionParser.Parse(positionInput);
        //        directionInput = GetCorrectDirectionFromUser(allShipsCoordinates, ship, positionInput);

        //        shipCoordinates = positionParser.WholeShipPositions(shipPosition, ship.Size, directionInput);
        //        isOut = positionValidator.IsOverRange(shipCoordinates);
        //        isOverlap = positionValidator.IsOverlap(shipCoordinates, allShipsCoordinates);
        //    }
        //    return shipCoordinates;
        //}







        //#endregion
    }
}
