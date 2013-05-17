namespace BalloonsPop
{
    using System.Text.RegularExpressions;

    public class Command : ICommand
    {
        private const string Separator = @"\s+";

        public Command(string command)
        {
            this.OriginalForm = command;
            this.Parse(command);
        }

        public CommandType Type { get; private set; }

        public string OriginalForm { get; private set; }

        public string[] Parameters { get; private set; }

        private void Parse(string command)
        {
            command.Trim();

            switch (command)
            {
                case "top":
                    this.Type = CommandType.Top;
                    break;
                case "restart":
                    this.Type = CommandType.Restart;
                    break;
                case "exit":
                    this.Type = CommandType.Exit;
                    break;
                default:
                    this.Type = CommandType.AttemptPop;
                    break;
            }

            this.Parameters = Regex.Split(command, Separator, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }
    }
}
