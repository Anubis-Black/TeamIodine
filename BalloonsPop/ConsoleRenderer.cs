namespace BalloonsPop
{
    using System;
    using System.Collections.Generic;

    public class ConsoleRenderer : IRenderer
    {
        public void RenderObjects(IList<IRenderable> renderableObjects)
        {
            Console.Clear();

            foreach (IRenderable renderableObject in renderableObjects)
            {
                Console.SetCursorPosition(renderableObject.Position.X, renderableObject.Position.Y);
                Console.BackgroundColor = this.MatchColour(renderableObject.Colour);

                if (Console.BackgroundColor == ConsoleColor.Black)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write(renderableObject.Visualisation);
            }
        }

        private ConsoleColor MatchColour(Colour colour)
        {
            ConsoleColor matchColour;

            switch (colour)
            {
                case Colour.Red:
                    matchColour = ConsoleColor.Red;
                    break;
                case Colour.Green:
                    matchColour = ConsoleColor.Green;
                    break;
                case Colour.Blue:
                    matchColour = ConsoleColor.Blue;
                    break;
                case Colour.Yellow:
                    matchColour = ConsoleColor.Yellow;
                    break;
                case Colour.Black:
                    matchColour = ConsoleColor.Black;
                    break;
                default:
                    throw new ArgumentException("Unknown colour type.");
            }

            return matchColour;
        }
    }
}
