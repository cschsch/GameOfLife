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

        public override IEnumerable<IWorld> Ticks() => Ticks(cells => new ClosedWorld(cells));

        protected override IEnumerable<Cell> GetNeighbours(Cell cell, int outerIndex, int innerIndex) =>
            Cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3))).Except(new[] {cell});

        public static Func<IWorld> NewWorld(int size) => () => new ClosedWorld(size);
    }
}