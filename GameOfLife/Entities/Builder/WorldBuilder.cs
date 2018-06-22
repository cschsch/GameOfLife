using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;

namespace GameOfLife.Entities.Builder
{
    public class WorldBuilder
    {
        private readonly World _value;
        private readonly IReadOnlyDictionary<string, dynamic> _defaultValues;

        public WorldBuilder()
        {
            _value = new World();
            _defaultValues = new Dictionary<string, dynamic>
            {
                {nameof(_value.Data), new WorldData()},
                {nameof(_value.NeighbourFinder), new OpenNeighbourFinder()},
                {nameof(_value.CellCalculator), new EnvironmentalCellCalculator(new Random())},
                {nameof(_value.Generator), new StandardWorldGenerator()}
            };
        }

        public WorldBuilder(World initial) : this() => _value = initial;

        public WorldBuilder WithData(WorldData data)
        {
            _value.Data = data;
            return this;
        }

        public WorldBuilder WithNeighbourFinder(IFindNeighbours neighbourFinder)
        {
            _value.NeighbourFinder = neighbourFinder;
            return this;
        }

        public WorldBuilder WithCellCalculator(ICalculateCell cellCalculator)
        {
            _value.CellCalculator = cellCalculator;
            return this;
        }

        public WorldBuilder WithGenerator(IGenerateWorld generator)
        {
            _value.Generator = generator;
            return this;
        }

        public World Create()
        {
            foreach (var property in typeof(World).GetProperties().Where(prop => prop.GetValue(_value) is null))
            {
                typeof(World).GetProperty(property.Name).SetValue(_value, _defaultValues[property.Name]);
            }

            return _value;
        }
    }
}