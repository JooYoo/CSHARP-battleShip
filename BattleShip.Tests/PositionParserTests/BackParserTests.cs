using System;
using System.Linq;
using BattleShip.DataContracts;
using BattleShip.Implementations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.PositionParserTests
{
    [TestClass]
    public class BackParserTests
    {
        [TestMethod]
        public void ReturnNotNull()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(0, 0);

            var output = positionParser.BackParser(thePosition);

            bool result;
            if (output != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            result.Should().Be(true);
        }


        [TestMethod]
        public void ReturnY()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(0, 0);

            string output = positionParser.BackParser(thePosition);

            string i = output.Last().ToString();

            i.Should().Be("1");
        }


        [TestMethod]
        public void ReturnX()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(0, 0);

            string output = positionParser.BackParser(thePosition);

            string i = output[0].ToString();

            i.Should().Be("A");
        }


        [TestMethod]
        public void Position44ReturnsE5()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(4, 4);

            string output = positionParser.BackParser(thePosition);


            output.Should().Be("E5");
        }

        [TestMethod]
        public void Position45ReturnsE6()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(4, 5);

            string output = positionParser.BackParser(thePosition);


            output.Should().Be("E6");
        }

        [TestMethod]
        public void Position46ReturnsE7()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(4, 6);

            string output = positionParser.BackParser(thePosition);


            output.Should().Be("E7");
        }

        [TestMethod]
        public void Position99ReturnsJ10()
        {
            PositionParser positionParser = new PositionParser();

            Position thePosition = new Position(9, 9);

            string output = positionParser.BackParser(thePosition);


            output.Should().Be("J10");
        }

    }
}