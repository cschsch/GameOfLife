using System;
using System.Collections.Generic;
using System.Linq;

using Engine.Entities.Standard.Builders;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Standard
{
    public class StandardCellGrid : BaseCellGrid<StandardCell>
    {
        public StandardCellGrid(IReadOnlyList<IReadOnlyList<StandardCell>> cells) => Cells = cells;

        public StandardCellGrid(string cellRep)
        {
            var cellRepToBuilder = new Dictionary<char, Func<IGenericBuilder<StandardCell>, IGenericBuilder<StandardCell>>>
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