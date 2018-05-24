using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{


    public class PositionValidator : IPositionValidator
    {
        public bool IsValidPosition(Position position, int columnSize, int rowSize, List<Ship> ships)
        {
            if (IsInputCorrectly(position, columnSize) == false)
            {
                return false;
            }

            if (IsOutOfBounds(position, columnSize, rowSize) || HasShipOnPosition(position, ships))
            {
                return false;
            }


            return true;
        }


        private static bool IsOutOfBounds(Position position, int columnSize, int rowSize)
        {

            return position.X >= columnSize || position.Y >= rowSize;
        }

        private static bool HasShipOnPosition(Position position, List<Ship> ships)
        {
            foreach (var ship in ships)
            {

                if (ship.Positions.Any(pos => pos.X == position.X && pos.Y == position.Y))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsInputCorrectly(Position rawPosition, int columnSize)
        {
            char letterChar;

            try // when Position(-2, 3)
            {
                letterChar = Convert.ToChar(rawPosition.X);
            }
            catch (Exception)
            {
                return false;
            }


            bool isLetter = !(letterChar < 65 || letterChar > 90);
            if (isLetter)
            {
                return false;
            }


            int number = rawPosition.Y;
            if (number > columnSize || number < 0)
            {
                return false;
            }


            return true;
        }



        public bool IsShootOk(Position ishootPosition, int columnSize, int rowSize, Player player)
        {
            if (ishootPosition == null) // Enter typo
            {
                return false;
            }

            if (IsInputCorrectly(ishootPosition,columnSize) == false)
            {
                return false;
            }

            if (IsOutOfBounds(ishootPosition,columnSize,rowSize))
            {
                return false;
            }

            if (HasAlreadyShoot(player,ishootPosition))
            {
                return false;
            }

            return true;
        }


        private static bool HasAlreadyShoot(Player player , Position ishootPosition )
        {
            if ( player.Hits.Contains(new Hit(HitType.Ship, ishootPosition)) || player.Hits.Contains(new Hit(HitType.Water, ishootPosition)))
            {
                return true;
            }

            return false;
        }
    }
}
