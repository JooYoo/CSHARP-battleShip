using BattleShip.DataContracts;

namespace BattleShip.OperationContracts
{
    public interface IRandomManager
    {
        Position RandomPosition(int columnSize, int rowSize);

        Direction RandomDirection();
    }
}