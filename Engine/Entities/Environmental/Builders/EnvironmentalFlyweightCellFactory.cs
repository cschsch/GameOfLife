using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Engine.Entities.Environmental.Builders
{
    public static class EnvironmentalFlyweightCellFactory
    {
        private static ConcurrentDictionary<string, EnvironmentalCell> Cache { get; }

        static EnvironmentalFlyweightCellFactory()
        {
            var values = new[]
            {
                new KeyValuePair<string, EnvironmentalCell>("AliveCarnivore", new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.Carnivore}),
                new KeyValuePair<string, EnvironmentalCell>("DeadCarnivore", new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.Carnivore}),
                new KeyValuePair<string, EnvironmentalCell>("AliveHerbivore", new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.Herbivore}),
                new KeyValuePair<string, EnvironmentalCell>("DeadHerbivore", new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.Herbivore}),
                new KeyValuePair<string, EnvironmentalCell>("AliveNone", new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.None}),
                new KeyValuePair<string, EnvironmentalCell>("DeadNone", new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.None})
            };

            Cache = new ConcurrentDictionary<string, EnvironmentalCell>(values);
        }

        public static EnvironmentalCell GetEnvironmentalCell(string key) => Cache[key];
        public static EnvironmentalCell GetEnvironmentalCell(string key, EnvironmentalCell cell) => Cache.GetOrAdd(key, cell);
    }
}