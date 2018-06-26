using System;
using System.Linq;

namespace GameOfLife.Helpers.Functions
{
    public static class GenericExtensions
    {
        public static void SetProperties<T>(this T objectToSet, T objectToGet)
        {
            var typeOfObjects = typeof(T);

            foreach (var property in typeOfObjects.GetProperties().Where(prop => prop.CanWrite))
            {
                var valueOfObjectToGet = typeOfObjects.GetProperty(property.Name).GetValue(objectToGet);
                property.SetValue(objectToSet, valueOfObjectToGet);
            }
        }
    }
}