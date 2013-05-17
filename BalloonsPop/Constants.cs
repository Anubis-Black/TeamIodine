namespace BalloonsPop
{
    public static class Constants
    {
        public const int TotalColumns = 10;
        public const int TotalRows = 5;
        public const int XOffset = 30;
        public const int YOffset = 8;
        public const int SpaceBetweenBalloons = 1;
        public const int IterationStep = SpaceBetweenBalloons + 1;
        public const int MaximalXPosition = IterationStep * TotalColumns;
        public const int MaximalYPosition = IterationStep * TotalRows;
    }
}
