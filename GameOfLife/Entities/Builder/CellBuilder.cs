using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Entities.Builder
{
    public class CellBuilder
    {
        private readonly Cell _value;
        private readonly IReadOnlyDictionary<string, dynamic> _defaultValues;

        public CellBuilder()
        {
            _value = new Cell();
            _defaultValues = new Dictionary<string, dynamic>
            {
                {nameof(_value.IsAlive), false },
                {nameof(_value.LifeTime), 0 },
                {nameof(_value.Diet), DietaryRestrictions.Carnivore }
            };
        }

        public CellBuilder(Cell initial)
        {
            _value = new Cell(initial);
            _defaultValues = new Dictionary<string, dynamic>
            {
                {nameof(_value.IsAlive), false },
                {nameof(_value.LifeTime), 0 },
                {nameof(_value.Diet), DietaryRestrictions.Carnivore }
            };
        }

        public CellBuilder WithAlive(bool isAlive)
        {
            _value.IsAlive = isAlive;
            return this;
        }

        public CellBuilder WithLifetime(int lifeTime)
        {
            _value.LifeTime = lifeTime;
            return this;
        }

        public CellBuilder WithDiet(DietaryRestrictions diet)
        {
            _value.Diet = diet;
            return this;
        }

        public Cell Create()
        {
            foreach (var property in typeof(Cell).GetProperties().Where(prop => prop.GetValue(_value) is null))
            {
                typeof(World).GetProperty(property.Name).SetValue(_value, _defaultValues[property.Name]);
            }

            return _value;
        }
    }
}