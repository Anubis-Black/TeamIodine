namespace BalloonsPop
{
    public interface IRenderable
    {
        Position Position { get; }

        char Visualisation { get; }

        Colour Colour { get; }
    }
}
