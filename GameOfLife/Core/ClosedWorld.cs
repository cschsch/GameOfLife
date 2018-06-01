using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core
{
    public class ClosedWorld : BaseWorld
    {
        public ClosedWorld(string cellRep) : base(cellRep) { }
        public ClosedWorld(ImmutableArray<ImmutableArray<Cell>> cells) : base(cells) { }
        public ClosedWorld(int size) : this(size, () => new Random()) { }
        public ClosedWorld(int size, Func<Random> randomFactory) : base(size, randomFactory) { }

        public override IEnumerable<IWorld> Ticks()
        {
            yield return this;
            var nextState = Cells.Select((row, outerInd) =>
                row.Select((cell, innerInd) =>
                    UpdateCell(cell, outerInd, innerInd)).ToImmutableArray()).ToImmutableArray();

            foreach (var state in new ClosedWorld(nextState).Ticks())
            {
                yield return state;
            }
        }

        protected override IEnumerable<Cell> GetNeighbours(Cell cell, int outerIndex, int innerIndex) =>
            Cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3))).Except(new[] {cell});
    }
}