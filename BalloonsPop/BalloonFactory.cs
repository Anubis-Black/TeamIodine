namespace BalloonsPop
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BalloonFactory : IFactory
    {
        public const int XOffset = 30;
        public const int YOffset = 8;
        public const int SpaceBetweenBalloons = 1;

        private const int MaximalXPosition = 2 * Engine.TotalColumns;
        private const int MaximalYPosition = 2 * Engine.TotalRows;

        private readonly char[] balloonVisualisations = { '1', '2', '3', '4' };

        public IList CreateObjects()
        {
            List<IRenderable> balloons = new List<IRenderable>();

            Array colours = Enum.GetValues(typeof(Colour));

            for (int row = 0; row < MaximalYPosition; row += 1 + SpaceBetweenBalloons)
            {
                for (int column = 0; column < MaximalXPosition; column += 1 + SpaceBetweenBalloons)
                {
                    Position position = new Position(column + XOffset, row + YOffset);

                    int randomisedBalloonNumber = RandomNumberGenerator.Instance.Next(this.balloonVisualisations.Length);

                    char visualisation = this.balloonVisualisations[randomisedBalloonNumber];

                    Colour colour = (Colour)colours.GetValue(randomisedBalloonNumber);

                    Balloon balloon = new Balloon(position, visualisation, colour);

                    balloons.Add(balloon);
                }
            }

            return balloons;
        }
    }
}
