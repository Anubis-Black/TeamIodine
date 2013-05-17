namespace BalloonsPop
{
    using System.Collections;
    using System.Collections.Generic;

    public class ImmovableObjectFactory : IFactory
    {
        private const int XOffset = Constants.XOffset - 3;
        private const int YOffset = Constants.YOffset - 2;

        private readonly char[] immovableObjectVisualisations = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public IList CreateObjects()
        {
            List<IRenderable> immovableObjects = new List<IRenderable>();

            for (int row = 0; row < Constants.MaximalYPosition; row += Constants.IterationStep)
            {
                Position position = new Position(XOffset, row + Constants.YOffset);

                char visualisation = this.immovableObjectVisualisations[(row >> 1)];

                ImmovableObject immovableObject = new ImmovableObject(position, visualisation);

                immovableObjects.Add(immovableObject);
            }

            for (int column = 0; column < Constants.MaximalXPosition; column += Constants.IterationStep)
            {
                Position position = new Position(column + Constants.XOffset, YOffset);

                char visualisation = this.immovableObjectVisualisations[(column >> 1)];

                ImmovableObject immovableObject = new ImmovableObject(position, visualisation);

                immovableObjects.Add(immovableObject);
            }

            return immovableObjects;
        }
    }
}
