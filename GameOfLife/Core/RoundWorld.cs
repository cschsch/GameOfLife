using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core
{
    public class RoundWorld : BaseWorld
    {
        private int Size => Cells.Length;

        public RoundWorld(string cellRep) : base(cellRep) { }
        public RoundWorld(ImmutableArray<ImmutableArray<Cell>> cells) : base(cells) { }
        public RoundWorld(int size) : this(size, () => new Random()) { }
        public RoundWorld(int size, Func<Random> randomFactory) : base(size, randomFactory) { }

        public override IEnumerable<IWorld> Ticks()
        {
            yield return this;
            var nextState = Cells.Select((row, outerInd) =>
                row.Select((cell, innerInd) =>
                    UpdateCell(cell, outerInd, innerInd)).ToImmutableArray()).ToImmutableArray();

            foreach (var state in new RoundWorld(nextState).Ticks())
            {
                yield return state;
            }
        }

        protected override IEnumerable<Cell> GetNeighbours(Cell cell, int outerIndex, int innerIndex) =>
            Cells.GetValues(Enumerable.Range(outerIndex - 1, 3).Select(ind => (ind + Size) % Size))
                .SelectMany(row => row.GetValues(Enumerable.Range(innerIndex - 1, 3).Select(ind => (ind + Size) % Size)))
                .Except(new[] {cell});

        //protected override IEnumerable<Cell> GetNeighbours(Cell cell, int outerIndex, int innerIndex) =>
        //    Cells.Cycle().Skip(Size - 1 + outerIndex).Take(3).SelectMany(row => row.Cycle().Skip(Size - 1 + innerIndex).Take(3)).Except(new[] {cell});
    }
}