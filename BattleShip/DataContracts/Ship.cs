using System.Collections.Generic;

namespace BattleShip.DataContracts
{
    public class Ship
    {
        public List<Position> Positions { get; }

        public int Size { get; }

        public ShipType ShipType { get; }

        public Ship(ShipType shipType, int size)
        {
            ShipType = shipType;
            Size = size;
            Positions = new List<Position>();
        }
    }
}
