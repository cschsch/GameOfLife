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