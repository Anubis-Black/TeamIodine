namespace BalloonsPop
{
    public struct Position
    {
        public Position(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        // For convenience this struct will be limited to integer positions only.
        public int X { get; set; }

        public int Y { get; set; }

        public static Position operator +(Position positionOne, Position positionTwo)
        {
            return new Position(positionOne.X + positionTwo.X, positionOne.Y + positionTwo.Y);
        }

        public static Position operator -(Position positionOne, Position positionTwo)
        {
            return new Position(positionOne.X - positionTwo.X, positionOne.Y - positionTwo.Y);
        }

        public static bool operator ==(Position positionOne, Position positionTwo)
        {
            return positionOne.X == positionTwo.X && positionOne.Y == positionTwo.Y;
        }

        public static bool operator !=(Position positionOne, Position positionTwo)
        {
            return positionOne.X != positionTwo.X || positionOne.Y != positionTwo.Y;
        }

        public override bool Equals(object otherObject)
        {
            if (otherObject is Position)
            {
                Position otherObjectPosition = (Position)otherObject;

                return this == otherObjectPosition;
            }

            return false;
        }

        public override int GetHashCode()
        {
            // In case the /checked option is set in the compiler. Overflow is not an issue.
            // Uniqueness is forced with a quadratic function in x and y using random primes.
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 29) + this.X.GetHashCode();
                hashCode = (hashCode * 29) + this.Y.GetHashCode();
                return hashCode;
            }
        }
    }
}
