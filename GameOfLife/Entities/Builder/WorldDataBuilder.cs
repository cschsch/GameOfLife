using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Entities.Builder
{
    public class WorldDataBuilder
    {
        private readonly WorldData _value;
        private readonly IReadOnlyDictionary<string, dynamic> _defaultValues;

        public WorldDataBuilder()
        {
            _value = new WorldData();
            _defaultValues = new Dictionary<string, dynamic>
            {
                {nameof(_value.Generation), 0 },
                {nameof(_value.Grid), new CellGrid(0)},
                {nameof(_value.HerbivoreDensity), 0 },
                {nameof(_value.Temperature), 0 }
            };
        }

        public WorldDataBuilder(WorldData initial) : this() => _value = initial;

        public WorldDataBuilder WithGeneration(int generation)
        {
            _value.Generation = generation;
            return this;
        }

        public WorldDataBuilder WithGrid(CellGrid grid)
        {
            _value.Grid = grid;
            return this;
        }

        public WorldDataBuilder WithTemperature(int temperature)
        {
            _value.Temperature = temperature;
            return this;
        }

        public WorldDataBuilder WithHerbivoreDensity(double herbivoreDensity)
        {
            _value.HerbivoreDensity = herbivoreDensity;
            return this;
        }

        public WorldData Create()
        {
            foreach (var property in typeof(WorldData).GetProperties().Where(prop => prop.GetValue(_value) is null))
            {
                typeof(World).GetProperty(property.Name).SetValue(_value, _defaultValues[property.Name]);
            }

            return _value;
        }
    }
}