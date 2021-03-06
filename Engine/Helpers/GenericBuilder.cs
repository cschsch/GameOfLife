﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Engine.Helpers
{
    public abstract class GenericBuilder<TObject> : IGenericBuilder<TObject>
    {
        protected readonly TObject ObjectToBuild;
        protected IReadOnlyDictionary<string, Func<dynamic>> DefaultValues;
        private readonly IReadOnlyDictionary<string, PropertyInfo> _properties;

        protected GenericBuilder(TObject objectToBuild)
        {
            ObjectToBuild = objectToBuild;
            _properties = new[] { typeof(TObject) }
                .Concat(typeof(TObject).GetInterfaces())
                .SelectMany(i => i
                    .GetProperties()
                    .Where(prop => prop.CanWrite))
                .ToDictionary(prop => prop.Name);
        }

        public IGenericBuilder<TObject> With<TValue>(Expression<Func<TObject, TValue>> property, TValue value)
        {
            var body = (MemberExpression) property.Body;
            var propertyName = body.Member.Name;
            _properties[propertyName].SetValue(ObjectToBuild, value);
            return this;
        }

        public TObject Create()
        {
            foreach (var property in typeof(TObject).GetProperties().Where(prop => prop.GetValue(ObjectToBuild) is null))
            {
                _properties[property.Name].SetValue(ObjectToBuild, DefaultValues[property.Name]());
            }

            return ObjectToBuild;
        }
    }
}