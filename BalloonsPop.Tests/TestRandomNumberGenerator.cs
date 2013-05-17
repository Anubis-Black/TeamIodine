namespace BalloonsPop.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestRandomNumberGenerator
    {
        [TestMethod]
        public void TestRandomOneToThree()
        {
            int iterationsCount = 100;

            bool actual = true;

            for (int iteration = 0; iteration < iterationsCount; iteration++)
            {
                int randomNumber = RandomNumberGenerator.Instance.Next(1, 4);

                if (randomNumber < 1 || randomNumber > 3)
                {
                    actual = false;
                }
            }

            bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRandomToSeventySeven()
        {
            int iterationsCount = 100;

            bool actual = true;

            for (int iteration = 0; iteration < iterationsCount; iteration++)
            {
                int randomNumber = RandomNumberGenerator.Instance.Next(78);

                if (randomNumber > 77)
                {
                    actual = false;
                }
            }

            bool expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}
