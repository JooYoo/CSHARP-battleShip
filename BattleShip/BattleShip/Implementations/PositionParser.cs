using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using BattleShip.OperationContracts;

namespace BattleShip
{
    public class PositionParser : IPositionParser
    {
        public Position Parse(string userInput)
        {
            if (userInput == "")
            {
                return null;
            }

            //all to Upper
            userInput = userInput.ToUpper();
            
            //Letter Digit
            char letterChar = userInput[0];
            const int charToPositionOffset = 65;
            int x = letterChar - charToPositionOffset;

            
            //Number Digit
            int number;
            //get the rest of Digits after letter
            string restDigit = userInput.Substring(1);
            bool isNumber = Int32.TryParse(restDigit, out number);
            if (isNumber == false )
            {
                return null;
            }
            int y = number - 1;

            return new Position(x, y);
        }

        public string BackParser(Position position)
        {
            int x = position.X + 65;
            int y = position.Y + 1;

            char xChar = (char)x;
            string yDigit = y.ToString();

            string result = xChar + yDigit;

            return result;

        }
    }
}
