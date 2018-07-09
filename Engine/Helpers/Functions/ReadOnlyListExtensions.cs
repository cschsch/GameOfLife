using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers.Functions
{
    public static class ReadOnlyListExtensions
    {
        /// <summary>
        /// Returns values of <paramref name="source"/> at the given <paramref name="indices"/>
        /// </summary>
        /// <typeparam name="T">Type of elements of <paramref name="source"/></typeparam>
        /// <param name="source">ReadOnlyList to return values from</param>
        /// <param name="indices">Sequence of indices representing positions in <paramref name="source"/></param>
        /// <returns>Sequence of elements at <paramref name="indices"/></returns>
        public static IEnumerable<T> GetValues<T>(this IReadOnlyList<T> source, IEnumerable<int> indices) =>
            indices.Select(i => source[i]);

        /// <summary>
        /// Returns values of <paramref name="source"/> at the given <paramref name="indices"/>, filtering only by those in bounds first
        /// </summary>
        /// <typeparam name="T">Type of elements of <paramref name="source"/></typeparam>
        /// <param name="source">ReadOnlyList to return values from</param>
        /// <param name="indices">Sequence of indices representing positions in <paramref name="source"/></param>
        /// <returns>Sequence of elements at <paramref name="indices"/></returns>
        public static IEnumerable<T> GetValuesSafe<T>(this IReadOnlyList<T> source, IEnumerable<int> indices) =>
            source.GetValues(indices.Where(i => i > 0 && i < source.Count));
    }
}