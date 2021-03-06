﻿using System;
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
                {EnvironmentalCell.Carnivore, builder => builder.With(c => c.Diet, DietaryRestriction.Carnivore)},
                {EnvironmentalCell.Herbivore, builder => builder.With(c => c.Diet, DietaryRestriction.Herbivore)}
            };

            Cells = cellRep.Split(Environment.NewLine).Select(row => row.Select(cRep =>
                    cellRepToBuilder.GetValueOrDefault(cRep, builder => builder)(new EnvironmentalCellBuilder().With(c => c.IsAlive, true)).Create())
                .ToArray()).ToArray();
        }

        public EnvironmentalCellGrid(int size)
        {
            var random = new Random();
            EnvironmentalCell GenerateCell() => new EnvironmentalCellBuilder()
                .With(c => c.IsAlive, random.NextBool())
                .With(c => c.Diet, random.NextBool() ? DietaryRestriction.Carnivore : DietaryRestriction.Herbivore)
                .Create();

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }
    }
}