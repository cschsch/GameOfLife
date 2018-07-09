using System;
using System.Linq;
using System.Linq.Expressions;

namespace Engine.Helpers.Functions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Gets all writable properties from <paramref name="objectToSet"/> and sets their values to those of <paramref name="objectToGet"/>
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="objectToSet"/> and <paramref name="objectToGet"/></typeparam>
        /// <param name="objectToSet">Object whose property values will be set</param>
        /// <param name="objectToGet">Object whose property values will be used</param>
        public static void SetProperties<T>(this T objectToSet, T objectToGet)
        {
            var typeOfObjects = typeof(T);

            foreach (var property in typeOfObjects.GetProperties().Where(prop => prop.CanWrite))
            {
                var valueOfObjectToGet = typeOfObjects.GetProperty(property.Name).GetValue(objectToGet);
                property.SetValue(objectToSet, valueOfObjectToGet);
            }
        }

        /// <summary>
        /// Gets the name of a given property selected by <paramref name="propertySelector"/>
        /// </summary>
        /// <typeparam name="T">Type of object whose property's name is to be gotten</typeparam>
        /// <typeparam name="TProperty">Type of the object's property</typeparam>
        /// <param name="propertySelector">Expression selecting the property</param>
        /// <returns>Name of the property</returns>
        public static string GetPropertyName<T, TProperty>(this Expression<Func<T, TProperty>> propertySelector)
        {
            var body = (MemberExpression) propertySelector.Body;
            return body.Member.Name;
        }
    }
}