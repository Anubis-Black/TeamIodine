namespace BalloonsPop.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestImmovableObjectFactory
    {
        [TestMethod]
        public void TestCreateObjects()
        {
            IFactory immovableObjectFactory = new ImmovableObjectFactory();

            var balloons = immovableObjectFactory.CreateObjects();

            bool containsOnlyBalloons = true;

            foreach (var renderableObject in balloons)
            {
                if (!(renderableObject is ImmovableObject))
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
