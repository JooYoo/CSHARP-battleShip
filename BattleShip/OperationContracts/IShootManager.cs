using System.Collections.Generic;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{
    public interface IShootManager
    {
        bool IsAllShipsSunken(List<Ship> ships);

        void RemoveHitPosition(Position iShootPosition, List<Ship> ships);

        bool IsSunken(Ship ship);

        Ship HittedShip(Position hitPosition, List<Ship> ships);

        void HitShip(Player player, Player computer, Position shootPosition, List<Ship> enermyShips,
            Battlefield battlefield);

        void HitWater(Player player, Player computer, Position iShootPosition, Battlefield battlefield,
            IPositionParser positionParser);

    }
}