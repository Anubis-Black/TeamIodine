namespace BalloonsPop.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestLauncher
    {
        [TestMethod]
        public void TestMain_Exit()
        {
            string inputCommand = "exit" + Environment.NewLine + "exit";

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    Launcher.Main();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }
    }
}
