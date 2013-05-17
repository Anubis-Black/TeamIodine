namespace BalloonsPop
{
    public class Balloon : GameObject
    {
        public Balloon(Position position, char visualisation, Colour colour) : base(position, visualisation, colour)
        {
        }

        public override void RespondToInteraction()
        {
            this.IsDestroyed = true;
        }

        public override void UpdatePosition(Position vectorChange)
        {
            this.Position += vectorChange;
        }
    }
}
