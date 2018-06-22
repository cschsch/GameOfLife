using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Core.Worlds
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
                {nameof(_value.Grid), new CellGrid(0)}
            };
        }

        public WorldDataBuilder(WorldData initial) : this() => _value = initial;

        public WorldDataBuilder WithGrid(CellGrid grid)
        {
            _value.Grid = grid;
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