using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers.Functions
{
    public static class EnumerablePrelude
    {
        public static IEnumerable<T> Repeat<T>(Func<T> elementGenerator, int count) =>
            Enumerable.Range(1, count).Select(_ => elementGenerator());
    }
}