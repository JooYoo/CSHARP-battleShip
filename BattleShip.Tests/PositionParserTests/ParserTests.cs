using System;
using BattleShip.DataContracts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShip.Tests.PositionParserTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void A1ReturnsPosition00()
        {
            var parser = new PositionParser();
            var input = "A1";
            var actual = parser.Parse(input);
            var expected = new Position(0, 0);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void A2ReturnsPosition01()
        {
            var parser = new PositionParser();
            var input = "A2";
            var actual = parser.Parse(input);
            var expected = new Position(0, 1);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void A3ReturnsPosition02()
        {
            var parser = new PositionParser();
            var input = "A3";
            var actual = parser.Parse(input);
            var expected = new Position(0, 2);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void A4ReturnsPosition03()
        {
            var parser = new PositionParser();
            var input = "A4";
            var actual = parser.Parse(input);
            var expected = new Position(0, 3);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void B1ReturnsPosition10()
        {
            var parser = new PositionParser();
            var input = "B1";
            var actual = parser.Parse(input);
            var expected = new Position(1, 0);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void B2ReturnsPosition11()
        {
            var parser = new PositionParser();
            var input = "B2";
            var actual = parser.Parse(input);
            var expected = new Position(1, 1);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void B3ReturnsPosition12()
        {
            var parser = new PositionParser();
            var input = "B3";
            var actual = parser.Parse(input);
            var expected = new Position(1, 2);

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod]
        public void B4ReturnsPosition13()
        {
            var parser = new PositionParser();
            var input = "B4";

            var actual = parser.Parse(input);
            var expected = new Position(1,3);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void Z1ReturnsPosition250()
        {
            var parser = new PositionParser();
            var input = "Z1";

            var actual = parser.Parse(input);
            var expected = new Position(25, 0);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void Z2ReturnsPosition251()
        {
            var parser = new PositionParser();
            var input = "Z2";

            var actual = parser.Parse(input);
            var expected = new Position(25, 1);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void Z3ReturnsPosition252()
        {
            var parser = new PositionParser();
            var input = "Z3";

            var actual = parser.Parse(input);
            var expected = new Position(25, 2);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void Z4ReturnsPosition253()
        {
            var parser = new PositionParser();
            var input = "Z4";

            var actual = parser.Parse(input);
            var expected = new Position(25, 3);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void a1ReturnsPosition00()
        {
            var parser = new PositionParser();
            var input = "a1";

            var actual = parser.Parse(input);
            var expected = new Position(0, 0);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void a2ReturnsPosition01()
        {
            var parser = new PositionParser();
            var input = "a2";

            var actual = parser.Parse(input);
            var expected = new Position(0, 1);

            expected.ShouldBeEquivalentTo(actual);
        }


        [TestMethod]
        public void a10ResturnsPosition09()
        {
            var parser = new PositionParser();
            var input = "a10";

            var actual = parser.Parse(input);
            var expected = new Position(0, 9);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod]
        public void AAResturnsThrowException()
        {
            var parser = new PositionParser();
            var input = "AA";

            var result = parser.Parse(input);

            result.Should().Be(null);
        }

        [TestMethod]
        public void ABResturnsThrowException()
        {
            var parser = new PositionParser();
            var input = "AB";

            var result = parser.Parse(input);

            result.Should().Be(null);
        }

        [TestMethod]
        public void ACResturnsThrowException()
        {
            var parser = new PositionParser();
            var input = "AC";

            var result = parser.Parse(input);

            result.Should().Be(null);
        }

        [TestMethod]
        public void A1B3ResturnsThrowException()
        {
            var parser = new PositionParser();
            var input = "A1C3";

            var result = parser.Parse(input);

            result.Should().Be(null);
        }

        [TestMethod]
        public void Input1AResturnsThrowException()
        {
            var parser = new PositionParser();
            var input = "1A";

            var result = parser.Parse(input);

            result.Should().Be(null);
        }

        

        

        [TestMethod]
        public void AAResturnsNull()
        {
            var parser = new PositionParser();
            var input = "AA";

            var actual = parser.Parse(input);

            actual.Should().Be(null);
        }

        


        [TestMethod]
        public void TwoMarktResturnsNull()
        {
            var parser = new PositionParser();
            var input = "?=";

            var actual = parser.Parse(input);

            actual.Should().Be(null);
        }

        [TestMethod]
        public void aaResturnsNull()
        {
            var parser = new PositionParser();
            var input = "aa";

            var actual = parser.Parse(input);

            actual.Should().Be(null);
        }

        

        [TestMethod]
        public void LetterNumLetterResturnsNull()
        {
            var parser = new PositionParser();
            var input = "b4h";

            var actual = parser.Parse(input);

            actual.Should().Be(null);
        }


        [TestMethod]
        public void EnterTypoReturnsNull()
        {
            var parser = new PositionParser();
            var input = "";

            var actual = parser.Parse(input);

            actual.Should().Be(null);
        }
    }
}
