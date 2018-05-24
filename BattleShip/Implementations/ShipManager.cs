using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{
    public class ShipManager : IShipManager
    {
        public List<Position> WholeShipPositions(Position startPoint, int shipSize, Direction direction)
        {
            List<Position> shipCoordinates = new List<Position>();

            if (direction == Direction.Horizontal)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    Position horizontalPosition = new Position(startPoint.X + i, startPoint.Y);
                    shipCoordinates.Add(horizontalPosition);
                }
            }
            else
            {
                for (int i = 0; i < shipSize; i++)
                {
                    Position verticalPosition = new Position(startPoint.X, startPoint.Y + i);
                    shipCoordinates.Add(verticalPosition);
                }
            }
            return shipCoordinates;
        }
    }
}
