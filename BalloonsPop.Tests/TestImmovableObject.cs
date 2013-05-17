using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BalloonsPop.Tests
{
    [TestClass]
    public class TestImmovableObject
    {
        [TestMethod]
        public void TestRespondToInteraction()
        {
            Position position = new Position(-31, 20);

            GameObject immovableObject = new ImmovableObject(position, '7');

            immovableObject.RespondToInteraction();

            bool actual = immovableObject.IsDestroyed;
            bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdatePosition()
        {
            Position originalPosition = new Position(24, 71);

            GameObject immovableObject = new ImmovableObject(originalPosition, '9');

            Position vectorChange = new Position(13, -7);

            immovableObject.UpdatePosition(vectorChange);

            Position actual = immovableObject.Position;
            Position expected = originalPosition;

            Assert.AreEqual(expected, actual);
        }
    }
}
