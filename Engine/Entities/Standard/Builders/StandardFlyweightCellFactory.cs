using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Engine.Entities.Standard.Builders
{
    public static class StandardFlyweightCellFactory
    {
        private static ConcurrentDictionary<string, StandardCell> Cache { get; }

        static StandardFlyweightCellFactory()
        {
            var values = new []
            {
                new KeyValuePair<string, StandardCell>("Alive", new StandardCell{IsAlive = true, LifeTime = 1}),
                new KeyValuePair<string, StandardCell>("Dead", new StandardCell{IsAlive = false, LifeTime = 0})
            };

            Cache = new ConcurrentDictionary<string, StandardCell>(values);
        }

        public static StandardCell GetStandardCell(string key) => Cache[key];
        public static StandardCell GetStandardCell(string key, StandardCell cell) => Cache.GetOrAdd(key, cell);
    }
}