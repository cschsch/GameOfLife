using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers.Functions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Divides <paramref name="source"/> into parts with a length of <paramref name="size"/>
        /// </summary>
        /// <typeparam name="T">Type of elements of <paramref name="source"/></typeparam>
        /// <param name="source">Sequence to partition</param>
        /// <param name="size">Size of each part</param>
        /// <returns>Partitioned sequence</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            if (!source.Skip(size - 1).Any()) yield break;
            yield return source.Take(size);
            foreach (var partition in source.Skip(size).Partition(size)) yield return partition;
        }

        /// <summary>
        /// Yields the given sequence, omitting the first instance of <paramref name="item"/>
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="item"/> and elements of <paramref name="source"/></typeparam>
        /// <param name="source">Sequence to return</param>
        /// <param name="item">Element to omit</param>
        /// <returns><paramref name="source"/> without first instance of <paramref name="item"/></returns>
        public static IEnumerable<T> SkipFirst<T>(this IEnumerable<T> source, T item)
        {
            var skipped = false;

            foreach (var element in source)
            {
                if (item.Equals(element) && !skipped)
                {
                    skipped = true;
                }
                else
                {
                    yield return element;
                }
            }
        }
    }
}