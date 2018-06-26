using System;
using System.Linq;

namespace GameOfLife.Helpers.Functions
{
    public static class GenericExtensions
    {
        public static void SetProperties<T>(this T objectToSet, T objectToGet)
        {
            foreach (var property in typeof(T).GetProperties().Where(prop => prop.CanWrite))
            {
                var valueOfObjectToGet = objectToGet.GetType().GetProperty(property.Name).GetValue(objectToGet);
                property.SetValue(objectToSet, valueOfObjectToGet);
            }
        }
    }
}