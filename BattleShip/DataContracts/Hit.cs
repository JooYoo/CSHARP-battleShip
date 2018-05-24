using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DataContracts
{
    public class Hit : IEquatable<Hit>
    {
        public Position Position { get;  }

        public HitType HitType { get; }


        public Hit(HitType hitType, Position position)
        {
            HitType = hitType;
            Position = position;
        }


        public bool Equals(Hit other)
        {
            if (this.HitType == other.HitType && this.Position.X == other.Position.X && this.Position.Y == other.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
