namespace BalloonsPop
{
    using System;

    public class Launcher
    {
        public static void Main()
        {
            Console.CursorVisible = false;

            IRenderer renderer = new ConsoleRenderer();

            Engine engine = new Engine(renderer);

            engine.Start();
        }
    }
}
