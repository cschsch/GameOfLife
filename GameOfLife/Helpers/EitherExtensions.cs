using Tp.Core;

namespace GameOfLife.Helpers
{
    public static class EitherExtensions
    {
        public static TLeft Left<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            var result = default(TLeft);
            either.Switch(left => result = left, _ => { });
            return result;
        }

        public static TRight Right<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            var result = default(TRight);
            either.Switch(_ => { }, right => result = right);
            return result;
        }
    }
}