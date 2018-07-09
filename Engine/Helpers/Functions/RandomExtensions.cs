using System;

namespace Engine.Helpers.Functions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Gets a random bool value according to <paramref name="propability"/>
        /// </summary>
        /// <param name="random">Random object</param>
        /// <param name="propability">Value starting from 0.0 representing the probability of receiving a true value. A value equal to 1 or larger always returns true</param>
        /// <returns>Bool value according to <paramref name="propability"/></returns>
        public static bool NextBool(this Random random, double propability)
        {
            if(propability < 0) throw new InvalidOperationException($"{nameof(propability)} must be larger than 0");
            return random.NextDouble() <= propability;
        }

        /// <summary>
        /// Gets a random bool value with a propability of 50%
        /// </summary>
        /// <param name="random">Random object</param>
        /// <returns>Random bool value</returns>
        public static bool NextBool(this Random random) => random.NextBool(0.5);
    }
}