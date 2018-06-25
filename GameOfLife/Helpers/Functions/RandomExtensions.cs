using System;

namespace GameOfLife.Helpers.Functions
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random, double propability)
        {
            if(propability < 0 || propability > 1) throw new InvalidOperationException($"{nameof(propability)} must be between 0 and 1");
            return random.NextDouble() <= propability;
        }

        public static bool NextBool(this Random random) => random.NextBool(0.5);
    }
}