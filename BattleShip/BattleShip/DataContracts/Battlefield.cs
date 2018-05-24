using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.DataContracts
{
    public class Battlefield
    {
        public int ColumnSize { get; }

        public int RowSize { get; }


        public Battlefield(int columnSize, int rowSize)
        {
            ColumnSize = columnSize;
            RowSize = rowSize;
        }

    }
}
