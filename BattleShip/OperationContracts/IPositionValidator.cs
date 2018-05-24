using System.Collections.Generic;
using BattleShip.DataContracts;

namespace BattleShip.OperationContracts
{
    public interface IPositionValidator
    {
        bool IsValidPosition(Position position, int columnSize, int rowSize, List<Ship> ships);

        bool IsShootOk(Position ishootPosition, int columnSize, int rowSize, Player player );

    }
}