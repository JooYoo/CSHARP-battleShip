using System.Collections.Generic;
using BattleShip.DataContracts;

namespace BattleShip.OperationContracts
{
    public interface IShipManager
    {
        List<Position> WholeShipPositions(Position startPoint, int shipSize, Direction direction);
    }
}