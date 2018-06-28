using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities.Standard.Builder;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Standard
{
    public class StandardCellGrid : BaseCellGrid<StandardCell>
    {
        public StandardCellGrid(IReadOnlyList<IReadOnlyList<StandardCell>> cells) => Cells = cells;

        public StandardCellGrid(string cellRep)
        {
            var cellRepToBuilder = new Dictionary<char, Func<GenericBuilder<StandardCell>, GenericBuilder<StandardCell>>>
            {
                {BaseCell.DeadIn, builder => builder.With(c => c.IsAlive, false)},
                {BaseCell.DeadOut, builder => builder.With(c => c.IsAlive, false)}
            };

            Cells = cellRep.Split(Environment.NewLine).Select(row => row.Select(cRep =>
                    cellRepToBuilder[cRep](new StandardCellBuilder().With(c => c.IsAlive, true)).Create())
                .ToArray()).ToArray();
        }

        public StandardCellGrid(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            StandardCell GenerateCell() => new StandardCellBuilder()
                .With(c => c.IsAlive, random.NextBool())
                .Create();

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }

        public StandardCellGrid(int size) : this(size, () => new Random()) { }
    }
}