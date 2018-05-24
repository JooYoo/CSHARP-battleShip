using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip.Implementations
{
    public static class GraphicManager
    {
        #region 1. [Welcome Screen]

        public static void WelcomeScreen()
        {
            Console.WriteLine();
            Console.WriteLine("                                                            Ä");
            Console.WriteLine("                                                           -|- ");
            Console.WriteLine("                                                            |  ");
            Console.WriteLine("                                                            | (+)");
            Console.WriteLine("                                                            |  |    ");
            Console.WriteLine("                                                        ___=====___");
            Console.WriteLine("                                                            /|");
            Console.WriteLine("                                  *                        / |");
            Console.WriteLine("                                  *                       /--|");
            Console.WriteLine(
                "                  *               |                  ====/---|====                      { ( ) }");
            Console.WriteLine(
                "                 ***              |                     /    |                         { (   ) }");
            Console.WriteLine(
                "                  |              [x]                   /-----|                          { ( ) }");
            Console.WriteLine(
                "                  |              [x]          ________/______|___[&][&]___                //");
            Console.WriteLine(
                "      *           |              [x]      ___|_________________[]_________|___           //");
            Console.WriteLine(
                "      H           |              [x]     /____________________________________\\         //");
            Console.WriteLine(
                "      H           |           ___[x]____/   [][]   [][][][]    [][][][][][][]  \\       //");
            Console.WriteLine(
                "      H           |          /     o   |    [][]   [][][][]    [][][][][][][]   \\-----//--");
            Console.WriteLine(
                "   ==============================================================================\\________\\___________");
            Console.WriteLine(
                "    \\--  o      ----------              ---------------------------------------------------------  O  /");
            Console.WriteLine(
                "     \\                                [=]                                          []                /");
            Console.WriteLine(
                "~~~~~~~~~~$~~~~~~~~~~~~~~~~~~~~~~%~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~&~~~~~~~~~~~~~~~~~~~~~~~~~~~~°°~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                                < BattleShip V 1.0 >");
            Console.WriteLine("                                              Press Enter to Continue...");
            Console.ReadLine();

            Console.Clear();
        }

        #endregion

        #region 2. [Allow placement of ships] Display One Battle field

        public static string DisplayBattlefield(int columnSize, int rowSize, List<Ship> ships)
        {
            var stringBuilder = new StringBuilder();
            BuildColumnHeaders(columnSize, stringBuilder);
            for (var row = 0; row < rowSize; row++)
            {
                BuildRowHeader(row, stringBuilder);

                BuildColumnRow(columnSize, ships, row, stringBuilder);

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }


        private static void BuildColumnRow(int columnSize, List<Ship> ships, int row, StringBuilder stringBuilder)
        {
            for (var column = 0; column < columnSize; column++)
            {
                if (HasShipOnPosition(new Position(column, row), ships))
                {
                    stringBuilder.Append(" #");
                }
                else
                {
                    stringBuilder.Append(" .");
                }
            }
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

        private static void BuildRowHeader(int row, StringBuilder stringBuilder)
        {
            string rowHeader = String.Format("{0}", row + 1);
            stringBuilder.AppendFormat("{0}", rowHeader.PadLeft(row < 10 ? 5 : 4));
        }

        private static void BuildColumnHeaders(int columnSize, StringBuilder stringBuilder)
        {
            // Column-Letter
            stringBuilder.Append("      ");
            for (var column = 0; column < columnSize; column++)
            {
                var letter = (char)('A' + column);
                stringBuilder.AppendFormat("{0} ", letter);
            }

            stringBuilder.AppendLine();
        }


        public static string DisplayPlaceTable(List<Ship> ships, ShipType currentShipType)
        {
            var stringBuilder = new StringBuilder();
            var shipGroups = ships.GroupBy(ship => ship.ShipType);

            stringBuilder.AppendLine(String.Format("{0}  {1} {2}", "#".PadLeft(5), "ship".PadRight(20), "size"));

            foreach (var shipGroup in shipGroups)
            {
                var ship = shipGroup.First();
                var arrowDisplay = ship.ShipType == currentShipType ? "=> ".PadLeft(4) : " ".PadLeft(4);
                stringBuilder.AppendLine(String.Format("{0}{1}x {2} {3}", arrowDisplay, shipGroup.Count(),
                    ship.ShipType.ToString().PadRight(20), ship.Size.ToString().PadLeft(2)));
            }

            return stringBuilder.ToString();
        }

        #endregion



        #region 4. [Player Shots] Display Double Batlle fields

        public static string DoubleBattleFieldsDisplayer(Player player, Player computer, int rowSize, int columnSize)
        {
            var stringBuilder = new StringBuilder();

            BuildFieldsHeaders(stringBuilder);

            BuildColumnHeadersPlayer(columnSize, stringBuilder);

            BuildColumnHeaderComputer(columnSize, stringBuilder);

            // BattleFields x 2
            for (int row = 0; row < rowSize; row++) //row
            {
                BuildRowHeadsPlayer(row, stringBuilder);
                BuildColumnRowPlayer(row, stringBuilder, player);

                BuildRowHeadsComputer(row, stringBuilder);
                BuildColumnRowComputer(computer, row, stringBuilder);

                stringBuilder.AppendLine(); // draw next line
            }

            return stringBuilder.ToString();
        }


        private static void BuildColumnRowComputer(Player computer, int row, StringBuilder stringBuilder)
        {
            // Row-Marks: RIGHT
            for (int column = 0; column < 10; column++)
            {
                if (computer.Hits.Contains(new Hit(HitType.Ship, new Position(column, row))))
                {
                    stringBuilder.Append("x ");
                }
                else if (computer.Hits.Contains(new Hit(HitType.Water, new Position(column, row))))
                {
                    stringBuilder.Append("~ ");
                }
                else
                {
                    stringBuilder.Append(". ");
                }
            }
        }

        private static void BuildRowHeadsComputer(int row, StringBuilder stringBuilder)
        {
            // Row-Number: RIGHT
            if (row < 9)
            {
                stringBuilder.Append("              ");
            }
            else
            {
                stringBuilder.Append("             ");
            }
            stringBuilder.Append(row + 1 + " ");
        }

        private static void BuildColumnHeaderComputer(int columnSize, StringBuilder stringBuilder)
        {
            // Column-Letter: RIGHT
            stringBuilder.Append("                "); // the gap between two Fields
            for (int i = 0; i < columnSize; i++)
            {
                char letter = (char)('A' + i);
                stringBuilder.Append(letter + " ");
            }
            stringBuilder.AppendLine(); // draw next Line
        }

        private static void BuildColumnRowPlayer( int row, StringBuilder stringBuilder, Player player)
        {
            // Row-Marks: LEFT
            for (var column = 0; column < 10; column++)
            {
                if (player.Positions.Contains(new Position(column, row))) // placeShip
                {
                    stringBuilder.Append("# ");
                }
                else if (player.Hits.Contains(new Hit(HitType.Ship, new Position(column, row)))) // hitPlayerShip
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    stringBuilder.Append(". ");
                }
                else if(player.Hits.Contains(new Hit(HitType.Water, new Position(column,row)))) // hitPlayerWater
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    stringBuilder.Append(". ");
                }
                else // placeWater
                {
                    stringBuilder.Append(". ");
                }
            }
        }

        private static void BuildRowHeadsPlayer(int row, StringBuilder stringBuilder)
        {
            // Row-Number: LEFT
            if (row < 9)
            {
                stringBuilder.Append("                           ");
            }
            else
            {
                stringBuilder.Append("                          ");
            }
            stringBuilder.Append(row + 1 + " ");
        }

        private static void BuildColumnHeadersPlayer(int columnSize, StringBuilder stringBuilder)
        {
            stringBuilder.Append("                             ");
            for (int i = 0; i < columnSize; i++)
            {
                char letter = (char)('A' + i);
                stringBuilder.Append(letter + " ");
            }
        }


        private static void BuildFieldsHeaders(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();

            // Column-Letter: LEFT
            stringBuilder.AppendLine(
                "                               [ Your Ships ]                      [ Enermy Ships ]");
            stringBuilder.AppendLine();
        }

        #endregion


        #region 4. [Player shots] Display Battle Tables

        public static  string DisplayBattleTables(List<Ship> playerShips, List<Ship> computerShips)
        {

            StringBuilder stringBuilder = new StringBuilder();

            BuildBeginSplitLine(stringBuilder);


            BuildTables(playerShips, computerShips, stringBuilder);


            BuildEndSplitLine(stringBuilder);


            return stringBuilder.ToString();
        }


        private static void BuildBeginSplitLine(StringBuilder stringBuilder)
        {
            stringBuilder.Append(
                "============================== YOUR FLEET ========================== PC FLEET =========================================");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
        }

        private static void BuildTables(List<Ship> playerShips, List<Ship> computerShips, StringBuilder stringBuilder)
        {
            for (int i = 0; i < playerShips.Count; i++)
            {
                // left space
                stringBuilder.Append("                          ");
                // Left Battle Table 
                if (playerShips[i].Positions.Count == 0)
                {
                    stringBuilder.Append("              ");
                }
                else
                {
                    stringBuilder.Append(playerShips[i].Size + "  " + playerShips[i].ShipType);
                }

                // middle space
                stringBuilder.Append("              ");

                // right Battle Table
                if (computerShips[i].Positions.Count == 0)
                {
                    stringBuilder.Append("");
                }
                else
                {
                    stringBuilder.Append(computerShips[i].ShipType.ToString().PadLeft(22) + "\t" +
                                         computerShips[i].Size.ToString().PadRight(22));
                }
                // newLine
                stringBuilder.AppendLine();
            }
        }

        private static void BuildEndSplitLine(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(
                "_______________________________________________________________________________________________________________________");

        }

        #endregion


        #region 5. [Player || PC shots]

        public static void GameStartView(Player player, Player computer, Battlefield battlefield)
        {
            // Display Graphic 
            DisplayBattleView(player, computer, battlefield);

            // information area
            Console.WriteLine("                                               ready to START");
            Console.Write("                                          Press Enter to continue >");
            Console.ReadKey();
            SoundEffects.SetShipSoundPlayer();
        }

        public static void DisplayBattleView(Player player, Player computer, Battlefield battlefield)
        {
            Console.Clear();

            // [ !!!!! ] DoubleBattleFieldsDisplayer(player, computer, RowSize, ColumnSize)
            // put Color On
            string graphicAsString = DoubleBattleFieldsDisplayer(player, computer, battlefield.RowSize, battlefield.ColumnSize);
            var redPointNumbers = RedPointNumbers(player);
            var bluePointNumbers = BluePointNumbers(player);
            for (int i = 0; i < graphicAsString.Length; i++)
            {
                if (redPointNumbers.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(graphicAsString[i]);
                }
                else if (bluePointNumbers.Contains(i))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(graphicAsString[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(graphicAsString[i]);
                }
            }

            Console.WriteLine();

            Console.WriteLine(DisplayBattleTables(player.Ships, computer.Ships));
        }

        private static List<int> RedPointNumbers(Player player)
        {
            List<int> colorPositionNumbers = new List<int>();

            foreach (var hit in player.Hits)
            {
                if (hit.HitType == HitType.Ship)
                {
                    int row = hit.Position.Y;
                    int column = hit.Position.X;
                    int colorPositionNumber = 205 + 87 * row + column * 2;

                    colorPositionNumbers.Add(colorPositionNumber);
                }
            }

            return colorPositionNumbers;
        }

        private static List<int> BluePointNumbers(Player player)
        {
            List<int> colorPositionNumbers = new List<int>();

            foreach (var hit in player.Hits)
            {
                if (hit.HitType == HitType.Water)
                {
                    int row = hit.Position.Y;
                    int column = hit.Position.X;
                    int colorPositionNumber = 205 + 87 * row + column * 2;

                    colorPositionNumbers.Add(colorPositionNumber);
                }
            }

            return colorPositionNumbers;
        }

        #endregion






        #region Not meet idea
        public static string WaitAmoment()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int sec = -1;


            while (stringBuilder.Length < 20)
            {

                if (sec != DateTime.Now.Second)
                {
                    sec = DateTime.Now.Second;
                    stringBuilder.Append("      ...");
                }
            }

            return stringBuilder.ToString();
        }

        public static void EmptyPlayField(int num)
        {
            Console.WriteLine();
            Console.WriteLine();

            // Column-Letter
            Console.Write("      ");
            for (int i = 0; i < num; i++)
            {
                char letter = (char)('A' + i);
                Console.Write(letter + " ");
            }
            Console.WriteLine();
            // PlayField points
            for (int row = 0; row < num; row++)
            {

                if (row < 9)
                {
                    Console.Write("    ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.Write(row + 1 + " "); // Row-Number

                for (int column = 0; column < num; column++)
                {
                    Console.Write(". ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void ShipStartPointDisplayer(int x, int y)
        {
            // Column-Letter
            Console.Write("   ");
            for (int i = 0; i < 10; i++)
            {
                char letter = (char)('A' + i);
                Console.Write(letter + " ");
            }
            Console.WriteLine();

            // PlayField
            for (int row = 0; row < 10; row++) //row
            {
                // Row-Number
                if (row < 9)
                {
                    Console.Write(" ");
                }
                Console.Write(row + 1 + " ");

                // place '.' or '#'
                for (int column = 0; column < 10; column++)
                {
                    if (column == x && row == y)
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }

                Console.WriteLine();
            }
        }

        #endregion

       
    }
}
