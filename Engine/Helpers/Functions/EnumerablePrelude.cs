using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers.Functions
{
    public static class EnumerablePrelude
    {
        /// <summary>
        /// Repeats <paramref name="elementGenerator"/> <paramref name="count"/> times, yielding a sequence
        /// </summary>
        /// <typeparam name="T">Type of elements to be generated</typeparam>
        /// <param name="elementGenerator">Function returning an element of type <typeparam name="T"></typeparam></param>
        /// <param name="count">Amount of times to repeat <paramref name="elementGenerator"/></param>
        /// <returns>Sequence of <typeparam name="T"></typeparam></returns>
        public static IEnumerable<T> Repeat<T>(Func<T> elementGenerator, int count) =>
            Enumerable.Range(1, count).Select(_ => elementGenerator());
    }
}