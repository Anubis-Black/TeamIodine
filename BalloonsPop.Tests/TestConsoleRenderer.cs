namespace BalloonsPop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestConsoleRenderer
    {
        [TestMethod]
        public void TestRenderObjects()
        {
            IRenderer consoleRenderer = new ConsoleRenderer();

            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                Console.SetOut(stringWriter);

                IList<IRenderable> renderableObjects = new List<IRenderable>
                {
                    new Balloon(new Position(30, 8), '1', Colour.Red),
                    new Balloon(new Position(32, 8), '3', Colour.Blue),
                    new Balloon(new Position(34, 8), '3', Colour.Blue),
                    new Balloon(new Position(30, 10), '2', Colour.Green),
                    new Balloon(new Position(32, 10), '1', Colour.Red),
                    new Balloon(new Position(34, 10), '2', Colour.Green),
                    new Balloon(new Position(30, 12), '4', Colour.Yellow),
                    new Balloon(new Position(32, 12), '4', Colour.Yellow),
                    new Balloon(new Position(34, 12), '4', Colour.Yellow),
                    new ImmovableObject(new Position(27, 8), '0'),
                    new ImmovableObject(new Position(27, 10), '1'),
                    new ImmovableObject(new Position(27, 12), '2'),
                    new ImmovableObject(new Position(30, 6), '0'),
                    new ImmovableObject(new Position(32, 6), '1'),
                    new ImmovableObject(new Position(34, 6), '2'),
                };

                consoleRenderer.RenderObjects(renderableObjects);

                string actual = stringBuilder.ToString();
                string expected = "133212444012012";

                Assert.AreEqual(expected, actual);
            }
        }
    }
}
