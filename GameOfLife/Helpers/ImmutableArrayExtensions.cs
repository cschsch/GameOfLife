using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GameOfLife.Helpers
{
    public static class ImmutableArrayExtensions
    {
        public static IEnumerable<T> GetValues<T>(this ImmutableArray<T> source, IEnumerable<int> indices) =>
            indices.Select(i => source[i]);

        public static IEnumerable<T> GetValues<T>(this ImmutableArray<T> source, params IEnumerable<int>[] indiceRanges) =>
            indiceRanges.SelectMany(ir => source.GetValues(ir));

        public static IEnumerable<T> GetValuesSafe<T>(this ImmutableArray<T> source, IEnumerable<int> indices) =>
            source.GetValues(indices.Where(i => i > 0 && i < source.Length));

        public static IEnumerable<T> GetValuesSafe<T>(this ImmutableArray<T> source, params IEnumerable<int>[] indiceRanges) =>
            indiceRanges.SelectMany(ir => source.GetValuesSafe(ir));
    }
}