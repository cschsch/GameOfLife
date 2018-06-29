using System;
using System.Linq.Expressions;
using Tp.Core;

namespace GameOfLife.Helpers
{
    public interface IGenericEitherBuilder<TLeft, TRight>
    {
        IGenericEitherBuilder<TLeft, TRight> With<TValue>(Maybe<Expression<Func<TLeft, TValue>>> ifLeft, Maybe<Expression<Func<TRight, TValue>>> ifRight, TValue value);
        IGenericEitherBuilder<TLeft, TRight> With<TValueLeft, TValueRight>(Expression<Func<TLeft, TValueLeft>> ifLeft, Expression<Func<TRight, TValueRight>> ifRight, Either<TValueLeft, TValueRight> values);
        Either<TLeft, TRight> Create();
    }
}
