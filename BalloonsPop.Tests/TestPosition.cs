namespace BalloonsPop.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestPosition
    {
        [TestMethod]
        public void TestOperatorPlus()
        {
            Position firstPosition = new Position(3, 5);
            Position secondPosition = new Position(-2, 6);

            Position actual = firstPosition + secondPosition;
            Position expected = new Position(1, 11);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOperatorMinus()
        {
            Position firstPosition = new Position(10, -5);
            Position secondPosition = new Position(3, 6);

            Position actual = firstPosition - secondPosition;
            Position expected = new Position(7, -11);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEquals_ObjectNotPosition()
        {
            Position position = new Position(3, 5);

            object notPosition = new object();

            bool actual = position.Equals(notPosition);
            bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOperatorNotEquals_DifferentPositions()
        {
            Position firstPosition = new Position(-3, 15);
            Position secondPosition = new Position(6, 7);

            bool actual = firstPosition != secondPosition;
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestHashCode()
        {
            Position position = new Position(13, 15);

            int hashCode = 17;

            hashCode = (hashCode * 29) + position.X.GetHashCode();
            hashCode = (hashCode * 29) + position.Y.GetHashCode();

            int actual = position.GetHashCode();
            int expected = hashCode;

            Assert.AreEqual(expected, actual);
        }
    }
}
