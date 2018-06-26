using System;

namespace GameOfLife.Helpers.Functions
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random, double propability)
        {
            if(propability < 0) throw new InvalidOperationException($"{nameof(propability)} must be larger than 0");
            return random.NextDouble() <= propability;
        }

        public static bool NextBool(this Random random) => random.NextBool(0.5);
    }
}