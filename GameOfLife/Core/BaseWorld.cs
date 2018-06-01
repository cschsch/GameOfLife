using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GameOfLife.Helpers;

namespace GameOfLife.Core
{
    public abstract class BaseWorld : IWorld
    {
        public ImmutableArray<ImmutableArray<Cell>> Cells { get; }

        protected BaseWorld(ImmutableArray<ImmutableArray<Cell>> cells) => Cells = cells;

        protected BaseWorld(string cellRep) =>
            Cells = cellRep.Split(Environment.NewLine)
                .Select(row => row.Select(c => c == ' ' ? new Cell(false, 0) : new Cell(true, 1)).ToImmutableArray())
                .ToImmutableArray();

        protected BaseWorld(int size) : this(size, () => new Random()) { }

        public override string ToString() =>
            Cells.Aggregate(new StringBuilder(),
                    (sb, row) => row.Aggregate(sb, (cellSb, cell) =>
                        cellSb.Append(cell)).Append(Environment.NewLine))
                .ToString();

        protected BaseWorld(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new Cell(random.NextDouble() >= 0.5, 1);
            
            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size)
                .Select(row => row.ToImmutableArray()).ToImmutableArray();
        }

        public abstract IEnumerable<IWorld> Ticks();

        protected IEnumerable<IWorld> Ticks(Func<ImmutableArray<ImmutableArray<Cell>>, IWorld> worldGenerator)
        {
            yield return this;
            var nextState = Cells.Select((row, outerInd) =>
                row.Select((cell, innerInd) =>
                    UpdateCell(cell, outerInd, innerInd)).ToImmutableArray()).ToImmutableArray();

            foreach (var state in worldGenerator(nextState).Ticks())
            {
                yield return state;
            }
        }

        protected Cell UpdateCell(Cell cell, int outerIndex, int innerIndex)
        {
            var alive = GetNeighbours(cell, outerIndex, innerIndex).Count(n => n.IsAlive);

            return new Match<Cell, Cell>(
                    (c => !c.IsAlive && (alive == 3 /*|| alive == 6*/), _ => new Cell(true, 1)),
                    (_ => alive < 2 || alive > 3, _ => new Cell(false, 0)),
                    (_ => true, c => new Cell(c.IsAlive, c.LifeTime + 1)))
                .MatchFirst(cell);
        }

        protected abstract IEnumerable<Cell> GetNeighbours(Cell cell, int outerIndex, int innerIndex);
    }
}