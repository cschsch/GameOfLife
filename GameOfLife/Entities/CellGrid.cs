using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities.Builder;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities
{
    public class CellGrid
    {
        public IReadOnlyList<IReadOnlyList<Cell>> Cells { get; }

        public CellGrid(IReadOnlyList<IReadOnlyList<Cell>> cells) => Cells = cells;

        public CellGrid(string cellRep)
        {
            var cellRepToBuilder = new Dictionary<char, Func<CellBuilder, CellBuilder>>
            {
                {Cell.DeadIn, builder => builder.WithAlive(false)},
                {Cell.Carnivore, builder => builder.WithDiet(DietaryRestrictions.Carnivore)},
                {Cell.Herbivore, cell => cell.WithDiet(DietaryRestrictions.Herbivore)}
            };

            Cells = cellRep.Split(Environment.NewLine).Select(row => row.Select(c =>
                    cellRepToBuilder[c](new CellBuilder().WithAlive(true)).Create())
                .ToArray()).ToArray();
        }

        public CellGrid(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new CellBuilder()
                .WithAlive(random.NextBool())
                .WithDiet(random.NextBool() ? DietaryRestrictions.Carnivore : DietaryRestrictions.Herbivore)
                .Create();

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }

        public CellGrid(int size) : this(size, () => new Random()) { }
    }
}