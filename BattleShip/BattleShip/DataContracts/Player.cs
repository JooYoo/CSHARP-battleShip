using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DataContracts
{
    public class Player
    {
        public List<Ship> Ships { get; set; }

        public List<Hit> Hits { get; set; }

        public List<Position> Positions { get; set; }
    }
}
