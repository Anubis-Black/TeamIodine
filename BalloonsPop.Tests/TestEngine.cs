namespace BalloonsPop.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestEngine
    {
        [TestMethod]
        public void TestPerformAttemptPopSuccessful()
        {
            string inputCommand = "" + Environment.NewLine + "3 3" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPerformInvalidCommand_InvalidInputOneParameter()
        {
            string inputCommand = "" + Environment.NewLine + "3;83" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPerformInvalidCommand_InvalidInputTwoParameters()
        {
            string inputCommand = "" + Environment.NewLine + "3 ;83" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPerformInvalidCommand_CellOutsideRange()
        {
            string inputCommand = "" + Environment.NewLine + "3 83" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestPerformInvalidMove_AlreadyPoppedBalloon()
        {
            string inputCommand = "" + Environment.NewLine + "0 0" + Environment.NewLine + "0 0" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRestartGame()
        {
            string inputCommand = "" + Environment.NewLine + "restart" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTop()
        {
            string inputCommand = "" + Environment.NewLine + "top" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCompleteGameEnterHighScore()
        {
            //TODO:
            string inputCommand = "" + Environment.NewLine + "top" + Environment.NewLine + "" + Environment.NewLine + "exit" + Environment.NewLine;

            StringBuilder stringBuilder = new StringBuilder();

            using (StringReader stringReader = new StringReader(inputCommand))
            {
                Console.SetIn(stringReader);

                using (StringWriter stringWriter = new StringWriter(stringBuilder))
                {
                    Console.SetOut(stringWriter);

                    IRenderer consoleRenderer = new ConsoleRenderer();

                    Engine engine = new Engine(consoleRenderer);

                    engine.Start();
                }
            }

            string gameData = stringBuilder.ToString();

            string actual = gameData.Substring(gameData.Length - 55);
            string expected = "Thank you for playing Balloons Pop! Have a great day!" + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }
    }
}
