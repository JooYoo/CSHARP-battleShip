using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{

    public class ShootManager : IShootManager
    {

        public bool IsAllShipsSunken(List<Ship> ships)
        {
            int sunkenAmount = 0;

            foreach (var ship in ships)
            {
                if (ship.Positions.Count == 0)
                {
                    sunkenAmount++;
                }
            }

            if (sunkenAmount == ships.Count)
            {
                return true;
            }

            return false;
        }


        public void RemoveHitPosition(Position iShootPosition, List<Ship> ships)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].Positions.Contains(iShootPosition))
                {
                    ships[i].Positions.Remove(iShootPosition);
                }
            }
        }

        public Ship HittedShip(Position hitPosition, List<Ship> ships)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].Positions.Contains(hitPosition))
                {
                    return ships[i];
                }
            }

            return null;
        }

        public bool IsSunken(Ship ship)
        {
            if (ship.Positions.Count == 0)
            {
                return true;
            }
            return false;
        }


        public static Position ComputerShoot(Player player, Player computer, Battlefield battlefield, IRandomManager randomManager, IPositionParser positionParser)
        {
            Console.Clear();
            GraphicManager.DisplayBattleView(player, computer, battlefield);
            // [!!!] should be better
            Position pcShootPosition = randomManager.RandomPosition(battlefield.ColumnSize, battlefield.RowSize);

            Console.WriteLine("                                                \" PC Shoot \" ");
            Console.WriteLine();
            Console.Write("                                                  THINKING");
            // imitate Thinking
            Thread.Sleep(1000);
            Console.Write("... ");
            Thread.Sleep(500);
            Console.Write("[ {0} ]", positionParser.BackParser(pcShootPosition));
            Thread.Sleep(500);
            Console.Write(" ...");
            Thread.Sleep(500);
            return pcShootPosition;
        }

        public static Position PlayerShoot(Player player, Player computer, Battlefield battlefield, IPositionParser positionParser, IPositionValidator positionValidator)
        {
            GraphicManager.DisplayBattleView(player, computer, battlefield);

            // isShootOk
            Position iShootPosition;
            do
            {
                Console.WriteLine("                                               \" I Shoot \" ");
                Console.WriteLine();
                Console.Write("                                                POSITION >");

                //convert to "Position"
                iShootPosition = positionParser.Parse(Console.ReadLine());
            } while (positionValidator.IsShootOk(iShootPosition, battlefield.ColumnSize, battlefield.RowSize, computer) == false);
            return iShootPosition;
        }


        public void HitShip(Player player, Player computer, Position shootPosition, List<Ship> enermyShips, Battlefield battlefield)
        {
            Ship hittedShip = HittedShip(shootPosition, enermyShips);

            // Display Graphic
            GraphicManager.DisplayBattleView(player, computer, battlefield);

            // remove Hit Position
            RemoveHitPosition(shootPosition, enermyShips);
            //isSunk or isHit
            SunkenHitInfo(hittedShip);
        }

        public void HitWater(Player player, Player computer, Position iShootPosition, Battlefield battlefield, IPositionParser positionParser)
        {
            // Display Graphic
            GraphicManager.DisplayBattleView(player, computer, battlefield);
            //Sound Effects
            SoundEffects.HitWaterSoundPlayer();
            //information
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                                               \" Shoot [ {0} ]\" ",
                positionParser.BackParser(iShootPosition));
            Console.WriteLine();
            Console.Write("                                                     LOST ");
            //Thread.Sleep(500);
            //Console.Write("...");
            //Thread.Sleep(1000);
            Console.ReadKey();
        }



        private void SunkenHitInfo(Ship hittedShip)
        {
            bool isSunken = IsSunken(hittedShip);
            if (isSunken)
            {
                //Sound Effects
                SoundEffects.SunkenSoundPlayer();
                //isSunken
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                                              \" SUNKEN !!! \" ");
                Console.WriteLine();
                Console.Write("                                                Continue >");
                Console.ReadKey();
            }
            else
            {
                //Sound Effects
                SoundEffects.HitShipSoundPlayer();
                // isHit
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("                                               \" HIT ! \" ");
                Console.WriteLine();
                Console.Write("                                                 LOAD ");
                
                Thread.Sleep(500);
                Console.Write("... ");
                Thread.Sleep(500);
                Console.Write("... ");
                Thread.Sleep(500);

                //Console.ReadKey();
            }
        }


    }
}
