namespace BalloonsPop
{
    public class ImmovableObject : GameObject
    {
        public ImmovableObject(Position position, char visualisation) : base(position, visualisation, Colour.Black)
        {
        }

        public override void RespondToInteraction()
        {
            return;
        }

        public override void UpdatePosition(Position vectorChange)
        {
            return;
        }
    }
}
