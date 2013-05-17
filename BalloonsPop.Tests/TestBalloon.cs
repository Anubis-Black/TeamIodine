namespace BalloonsPop.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestBalloon
    {
        [TestMethod]
        public void TestRespondToInteraction()
        {
            Position position = new Position(23, 34);

            GameObject balloon = new Balloon(position, '2', Colour.Green);

            balloon.RespondToInteraction();

            bool actual = balloon.IsDestroyed;
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdatePosition()
        {
            Position position = new Position(17, 42);

            GameObject balloon = new Balloon(position, '1', Colour.Red);

            Position vectorChange = new Position(20, -3);

            balloon.UpdatePosition(vectorChange);

            Position actual = balloon.Position;
            Position expected = new Position(37, 39);

            Assert.AreEqual(expected, actual);
        }
    }
}
