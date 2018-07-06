using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers.Functions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            if (!source.Skip(size - 1).Any()) yield break;
            yield return source.Take(size);
            foreach (var partition in source.Skip(size).Partition(size)) yield return partition;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> source, T item, int amount)
        {
            var currentlySkipped = 0;

            foreach (var element in source)
            {
                if (item.Equals(element) && currentlySkipped < amount)
                {
                    currentlySkipped++;
                }
                else
                {
                    yield return element;
                }
            }
        }
    }
}