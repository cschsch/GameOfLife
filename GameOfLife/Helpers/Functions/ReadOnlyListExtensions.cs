﻿using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Helpers.Functions
{
    public static class ReadOnlyListExtensions
    {
        public static IEnumerable<T> GetValues<T>(this IReadOnlyList<T> source, IEnumerable<int> indices) =>
            indices.Select(i => source[i]);

        public static IEnumerable<T> GetValuesSafe<T>(this IReadOnlyList<T> source, IEnumerable<int> indices) =>
            source.GetValues(indices.Where(i => i > 0 && i < source.Count));
    }
}