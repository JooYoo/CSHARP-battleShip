using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;

namespace BattleShip.OperationContracts
{
    public interface IPlaceManager
    {
        void PlacePlayerShips(List<Ship> playerShips, IPositionParser positionParser,
            IPositionValidator positionValidator, IShipManager shipManager, Battlefield battlefield);

        void PlaceComputerShips(List<Ship> computerShips, IRandomManager randomManager, IShipManager shipManager,
            IPositionValidator positionValidator, Battlefield battlefield);

    }
}
