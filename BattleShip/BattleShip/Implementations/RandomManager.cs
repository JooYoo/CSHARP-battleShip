using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{
    public class RandomManager : IRandomManager
    {
        private readonly Random random;

        public RandomManager()
        {
            random = new Random();
        }

        public Position RandomPosition(int columnSize, int rowSize)
        {
            var coordinateX = random.Next(0, columnSize);
            var coordinateY = random.Next(0, rowSize);

            return new Position(coordinateX, coordinateY);
        }

        public Direction RandomDirection()
        {
            var directionIndex = random.Next(1, 3);
            return (Direction) directionIndex;
        }
    }
}
