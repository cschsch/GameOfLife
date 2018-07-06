using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Helpers
{
    public struct Match<T, TResult>
    {
        private IEnumerable<(Predicate<T> Condition, Func<T, TResult> Result)> Matches { get; }

        public Match(params (Predicate<T>, Func<T, TResult>)[] matches) => Matches = matches;

        public TResult MatchFirst(T item) => Matches.First(predicate => predicate.Condition(item)).Result(item);

        public static TResult MatchFirst(T item, params (Predicate<T> Condition, Func<T, TResult> Result)[] matches) =>
            matches.First(predicate => predicate.Condition(item)).Result(item);
    }
}