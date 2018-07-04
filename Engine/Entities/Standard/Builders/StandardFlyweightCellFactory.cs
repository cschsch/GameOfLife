using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Engine.Entities.Standard.Builders
{
    public static class StandardFlyweightCellFactory
    {
        private static ConcurrentDictionary<string, Lazy<StandardCell>> Cache { get; }

        static StandardFlyweightCellFactory()
        {
            var values = new []
            {
                new KeyValuePair<string, Lazy<StandardCell>>("Alive", new Lazy<StandardCell>(() => new StandardCell{IsAlive = true, LifeTime = 1}) ),
                new KeyValuePair<string, Lazy<StandardCell>>("Dead", new Lazy<StandardCell>(() => new StandardCell{IsAlive = false, LifeTime = 0}) )
            };

            Cache = new ConcurrentDictionary<string, Lazy<StandardCell>>(values);
        }

        public static StandardCell GetStandardCell(string key) => Cache[key].Value;
    }
}