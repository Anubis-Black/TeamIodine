namespace BalloonsPop.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestCommand
    {
        [TestMethod]
        public void TestCommandTypeTop()
        {
            string top = "top";

            ICommand command = new Command(top);

            CommandType actual = command.Type;
            CommandType expected = CommandType.Top;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestCommandTypeRestart()
        {
            string restart = "restart";

            ICommand command = new Command(restart);

            CommandType actual = command.Type;
            CommandType expected = CommandType.Restart;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCommandTypeExit()
        {
            string exit = "exit";

            ICommand command = new Command(exit);

            CommandType actual = command.Type;
            CommandType expected = CommandType.Exit;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCommandTypeAttemptPop_Correct()
        {
            string attemptPop = "3 0";

            ICommand command = new Command(attemptPop);

            CommandType actual = command.Type;
            CommandType expected = CommandType.AttemptPop;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCommandTypeAttemptPop_Incorrect()
        {
            string roflmao = "roflmao";

            ICommand command = new Command(roflmao);

            CommandType actual = command.Type;
            CommandType expected = CommandType.AttemptPop;

            Assert.AreEqual(expected, actual);
        }
    }
}
