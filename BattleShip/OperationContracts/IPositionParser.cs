using System.Collections.Generic;
using BattleShip.DataContracts;

namespace BattleShip.OperationContracts
{
    public interface IPositionParser
    {
        string BackParser(Position position);

        Position Parse(string userInput);

    }
}