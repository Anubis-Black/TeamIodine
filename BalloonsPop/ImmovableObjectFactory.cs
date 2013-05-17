namespace BalloonsPop
{
    using System.Collections;
    using System.Collections.Generic;

    public class ImmovableObjectFactory : IFactory
    {
        private const int MaximalXPosition = 2 * Engine.TotalColumns;
        private const int MaximalYPosition = 2 * Engine.TotalRows;
        private const int XOffset = BalloonFactory.XOffset - 3;
        private const int YOffset = BalloonFactory.YOffset - 2;

        private readonly char[] immovableObjectVisualisations = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public IList CreateObjects()
        {
            List<IRenderable> immovableObjects = new List<IRenderable>();

            for (int row = 0; row < MaximalYPosition; row += 1 + BalloonFactory.SpaceBetweenBalloons)
            {
                Position position = new Position(XOffset, row + BalloonFactory.YOffset);

                char visualisation = this.immovableObjectVisualisations[(row >> 1)];

                ImmovableObject immovableObject = new ImmovableObject(position, visualisation);

                immovableObjects.Add(immovableObject);
            }

            for (int column = 0; column < MaximalXPosition; column += 1 + BalloonFactory.SpaceBetweenBalloons)
            {
                Position position = new Position(column + BalloonFactory.XOffset, YOffset);

                char visualisation = this.immovableObjectVisualisations[(column >> 1)];

                ImmovableObject immovableObject = new ImmovableObject(position, visualisation);

                immovableObjects.Add(immovableObject);
            }

            return immovableObjects;
        }
    }
}
