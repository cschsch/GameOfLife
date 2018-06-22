using System;

namespace GameOfLife.Helpers
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random, double propability) => random.NextDouble() <= propability;
        public static bool NextBool(this Random random) => random.NextBool(0.5);
    }
}