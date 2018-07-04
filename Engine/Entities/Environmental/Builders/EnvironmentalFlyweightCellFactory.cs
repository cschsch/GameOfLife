using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Engine.Entities.Environmental.Builders
{
    public static class EnvironmentalFlyweightCellFactory
    {
        private static ConcurrentDictionary<string, Lazy<EnvironmentalCell>> Cache { get; }

        static EnvironmentalFlyweightCellFactory()
        {
            var values = new[]
            {
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("AliveCarnivore", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.Carnivore})),
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("DeadCarnivore", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.Carnivore})),
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("AliveHerbivore", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.Herbivore})),
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("DeadHerbivore", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.Herbivore})),
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("AliveNone", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = true, LifeTime = 1, Diet = DietaryRestriction.None})),
                new KeyValuePair<string, Lazy<EnvironmentalCell>>("DeadNone", new Lazy<EnvironmentalCell>(() => new EnvironmentalCell {IsAlive = false, LifeTime = 0, Diet = DietaryRestriction.None}))
            };

            Cache = new ConcurrentDictionary<string, Lazy<EnvironmentalCell>>(values);
        }

        public static EnvironmentalCell GetEnvironmentalCell(string key) => Cache[key].Value;
    }
}