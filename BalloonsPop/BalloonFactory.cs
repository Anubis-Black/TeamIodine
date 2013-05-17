namespace BalloonsPop
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BalloonFactory : IFactory
    {      
        private readonly char[] balloonVisualisations = { '1', '2', '3', '4' };

        public IList CreateObjects()
        {
            List<IRenderable> balloons = new List<IRenderable>();

            Array colours = Enum.GetValues(typeof(Colour));

            for (int row = 0; row < Constants.MaximalYPosition; row += Constants.IterationStep)
            {
                for (int column = 0; column < Constants.MaximalXPosition; column += Constants.IterationStep)
                {
                    Position position = new Position(column + Constants.XOffset, row + Constants.YOffset);

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
