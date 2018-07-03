using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using Tp.Core;

namespace GameOfLife.Helpers
{
    public class GenericEitherBuilder<T1, T2> : IGenericEitherBuilder<T1, T2>
    {
        private Either<T1, T2> _leftOrRight;
        private T1 _valueOfLeft;
        private T2 _valueOfRight;
        private readonly IReadOnlyDictionary<string, PropertyInfo> _propertiesLeft;
        private readonly IReadOnlyDictionary<string, PropertyInfo> _propertiesRight;

        public GenericEitherBuilder(Either<T1, T2> value)
        {
            IReadOnlyDictionary<string, PropertyInfo> GetPropertiesWithNames<T>() =>
                new[] { typeof(T) }
                .Concat(typeof(T).GetInterfaces())
                .SelectMany(i => i
                    .GetProperties()
                    .Where(prop => prop.CanWrite))
                .ToDictionary(prop => prop.Name);

            value.Switch(left => _leftOrRight = Either.CreateLeft<T1, T2>(left), right => _leftOrRight = Either.CreateRight<T1, T2>(right));
            value.Switch(left => _valueOfLeft = left, right => _valueOfRight = right);
            _propertiesLeft = GetPropertiesWithNames<T1>();
            _propertiesRight = GetPropertiesWithNames<T2>();
        }

        public IGenericEitherBuilder<T1, T2> With<TValue>(Maybe<Expression<Func<T1, TValue>>> ifLeft, Maybe<Expression<Func<T2, TValue>>> ifRight, TValue value) =>
            _leftOrRight.Switch(
                _ => ifLeft.HasValue ? WithLeft(ifLeft.Value, value) : this,
                _ => ifRight.HasValue ? WithRight(ifRight.Value, value) : this);

        public IGenericEitherBuilder<T1, T2> With<TValueLeft, TValueRight>(Expression<Func<T1, TValueLeft>> ifLeft, Expression<Func<T2, TValueRight>> ifRight, Either<TValueLeft, TValueRight> values) =>
            values.Switch(left => WithLeft(ifLeft, left), right => WithRight(ifRight, right));

        private IGenericEitherBuilder<T1, T2> WithLeft<TValue>(Expression<Func<T1, TValue>> property, TValue value)
        {
            var body = (MemberExpression) property.Body;
            var propertyName = body.Member.Name;
            _propertiesLeft[propertyName].SetValue(_valueOfLeft, value);
            return this;
        }

        private IGenericEitherBuilder<T1, T2> WithRight<TValue>(Expression<Func<T2, TValue>> property, TValue value)
        {
            var body = (MemberExpression) property.Body;
            var propertyName = body.Member.Name;
            _propertiesRight[propertyName].SetValue(_valueOfRight, value);
            return this;
        }

        public Either<T1, T2> Create() =>
            _leftOrRight.Switch(_ => Either.CreateLeft<T1, T2>(_valueOfLeft), _ => Either.CreateRight<T1, T2>(_valueOfRight));
    }
}
