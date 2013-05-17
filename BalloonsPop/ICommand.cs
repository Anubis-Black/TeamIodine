namespace BalloonsPop
{
    public interface ICommand
    {
        CommandType Type { get; }

        string OriginalForm { get; }

        string[] Parameters { get; }
    }
}
