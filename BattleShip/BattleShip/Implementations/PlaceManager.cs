using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{
    public class PlaceManager : IPlaceManager
    {
        public void PlacePlayerShips(List<Ship> playerShips, IPositionParser positionParser, IPositionValidator positionValidator, IShipManager shipManager, Battlefield battlefield)
        {
            foreach (var ship in playerShips)
            {
                Console.Clear();
                Console.WriteLine();
                //display battle field
                Console.WriteLine(GraphicManager.DisplayBattlefield(battlefield.ColumnSize, battlefield.RowSize, playerShips));
                //display place table
                Console.WriteLine(GraphicManager.DisplayPlaceTable(playerShips, ship.ShipType));

                //get the StartPosition
                Position position;
                do
                {
                    Console.Write(" Enter position for {0} > ", ship.ShipType);
                    position = positionParser.Parse(Console.ReadLine());

                } while (position == null);

                //get the direction
                Direction direction;
                do
                {
                    direction = GetDirection();

                } while (direction == Direction.None);

                //get whole ship positions
                var shipPositions = shipManager.WholeShipPositions(position, ship.Size, direction);

                //validate player position
                shipPositions = ValidatePlayerPositions(playerShips, positionParser, positionValidator, shipManager, shipPositions, ship, battlefield);

                ship.Positions.AddRange(shipPositions);
                SoundEffects.SetShipSoundPlayer();
            }
        }

        public void PlaceComputerShips(List<Ship> computerShips, IRandomManager randomManager, IShipManager shipManager, IPositionValidator positionValidator, Battlefield battlefield)
        {
            foreach (var ship in computerShips)
            {
                var randomPosition = randomManager.RandomPosition(battlefield.ColumnSize, battlefield.RowSize);
                var randomDirection = randomManager.RandomDirection();
                var shipPositions = shipManager.WholeShipPositions(randomPosition, ship.Size, randomDirection);

                for (int i = 0; i < shipPositions.Count; i++)
                {
                    while (positionValidator.IsValidPosition(shipPositions[i], battlefield.ColumnSize, battlefield.RowSize, computerShips) == false)
                    {
                        randomPosition = randomManager.RandomPosition(battlefield.ColumnSize, battlefield.RowSize);
                        randomDirection = randomManager.RandomDirection();
                        shipPositions = shipManager.WholeShipPositions(randomPosition, ship.Size, randomDirection);
                        i = -1;
                        break;
                    }
                }

                ship.Positions.AddRange(shipPositions);
            }
        }


        private static Direction GetDirection()
        {
            Console.Write(" (V)ertical or (H)orizontal >");
            string directionInput = Console.ReadLine().ToLower();

            if (directionInput == "v")
            {
                return Direction.Vertical;
            }

            if (directionInput == "h")
            {
                return Direction.Horizontal;
            }

            return Direction.None;
        }


        private static List<Position> ValidatePlayerPositions(List<Ship> playerShips, IPositionParser positionParser, IPositionValidator positionValidator, IShipManager shipManager, List<Position> shipPositions, Ship ship, Battlefield battlefield)
        {
            Position position;
            Direction direction;
            for (int i = 0; i < shipPositions.Count; i++)
            {
                while (positionValidator.IsValidPosition(shipPositions[i], battlefield.ColumnSize, battlefield.RowSize, playerShips) == false)
                {
                    Console.WriteLine();
                    do
                    {
                        Console.Write(" Position is not validated, try again >");
                        position = positionParser.Parse(Console.ReadLine());
                    } while (position == null);

                    do
                    {
                        direction = PlaceManager.GetDirection();
                    } while (direction == Direction.None);

                    shipPositions = shipManager.WholeShipPositions(position, ship.Size, direction);

                    i = -1;
                    break;
                }
            }
            return shipPositions;
        }
    }
}
