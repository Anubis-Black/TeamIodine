namespace BalloonsPop.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestBalloonFactory
    {
        [TestMethod]
        public void TestCreateObjects()
        {
            IFactory balloonFactory = new BalloonFactory();

            var balloons = balloonFactory.CreateObjects();

            bool containsOnlyBalloons = true;

            foreach (var renderableObject in balloons)
            {
                if (!(renderableObject is Balloon))
                {
                    containsOnlyBalloons = false;
                    break;
                }
            }

            bool actual = containsOnlyBalloons;
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}
