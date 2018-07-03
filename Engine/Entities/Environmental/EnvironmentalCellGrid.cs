using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Entities.Environmental.Builders;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalCellGrid : BaseCellGrid<EnvironmentalCell>
    {
        public EnvironmentalCellGrid(IReadOnlyList<IReadOnlyList<EnvironmentalCell>> cells) => Cells = cells;

        public EnvironmentalCellGrid(string cellRep)
        {
            var cellRepToBuilder = new Dictionary<char, Func<IGenericBuilder<EnvironmentalCell>, IGenericBuilder<EnvironmentalCell>>>
            {
                {BaseCell.DeadIn, builder => builder.With(c => c.IsAlive, false)},
                {BaseCell.DeadOut, builder => builder.With(c => c.IsAlive, false) },
                {EnvironmentalCell.Carnivore, builder => builder.With(c => c.Diet, DietaryRestrictions.Carnivore)},
                {EnvironmentalCell.Herbivore, builder => builder.With(c => c.Diet, DietaryRestrictions.Herbivore)}
            };

            Cells = cellRep.Split(Environment.NewLine).Select(row => row.Select(cRep =>
                    cellRepToBuilder[cRep](new EnvironmentalCellBuilder().With(c => c.IsAlive, true)).Create())
                .ToArray()).ToArray();
        }

        public EnvironmentalCellGrid(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            EnvironmentalCell GenerateCell() => new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, random.NextBool())
                .With(c => c.Diet, random.NextBool() ? DietaryRestrictions.Carnivore : DietaryRestrictions.Herbivore)
                .Create();

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }

        public EnvironmentalCellGrid(int size) : this(size, () => new Random()) { }
    }
}