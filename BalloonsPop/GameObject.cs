namespace BalloonsPop
{
    public abstract class GameObject : IRenderable
    {
        protected GameObject(Position position, char visualisation, Colour colour)
        {
            this.Position = position;
            this.Visualisation = visualisation;
            this.Colour = colour;
        }

        public Position Position { get; protected set; }

        public char Visualisation { get; protected set; }

        public Colour Colour { get; protected set; }

        public bool IsDestroyed { get; protected set; }

        public abstract void RespondToInteraction();

        public abstract void UpdatePosition(Position vectorChange);
    }
}
