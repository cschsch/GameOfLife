using System;
using System.Linq.Expressions;

namespace Engine.Helpers
{
    public interface IGenericBuilder<TObject>
    {
        IGenericBuilder<TObject> With<TValue>(Expression<Func<TObject, TValue>> property, TValue value);
        TObject Create();
    }
}