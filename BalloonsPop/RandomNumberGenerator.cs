namespace BalloonsPop
{
    using System;

    public class RandomNumberGenerator
    {
        private static readonly Random RNGInstance;

        static RandomNumberGenerator()
        {
            RNGInstance = new Random();
        }

        private RandomNumberGenerator()
        {
        }

        public static Random Instance
        {
            get
            {
                return RNGInstance;
            }
        }
    }
}
