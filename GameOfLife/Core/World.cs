using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GameOfLife.Helpers;
using Tp.Core;

namespace GameOfLife.Core
{
    public class World
    {
        public ImmutableArray<ImmutableArray<Cell>> Cells { get; }
        public IGetNeighbours Neighbours { get; }

        private World(Maybe<ImmutableArray<ImmutableArray<Cell>>> cells, Maybe<IGetNeighbours> neighbours)
        {
            Cells = cells.GetOrDefault();
            Neighbours = neighbours.GetOrDefault();
        }

        public World() { }
        public World WithNeighbour(IGetNeighbours neighbours) => new World(Maybe.Return(Cells), Maybe.Return(neighbours));
        public World WithCells(ImmutableArray<ImmutableArray<Cell>> cells) => new World(Maybe.Return(cells), Maybe.Return(Neighbours));

        public World(string cellRep) =>
            Cells = ImmutableArray.CreateRange(cellRep.Split(Environment.NewLine).Select(row =>
                    ImmutableArray.CreateRange(row.Select(c => c == ' ' ? new Cell(false, 0) : new Cell(true, 1)))));

        public World(int size) : this(size, () => new Random()) { }

        public World(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new Cell(random.NextDouble() >= 0.5, 1);

            Cells = ImmutableArray.CreateRange(EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size)
                .Select(ImmutableArray.CreateRange));
        }

        public override string ToString() =>
            Cells.Aggregate(new StringBuilder(),
                    (sb, row) => row.Aggregate(sb, (cellSb, cell) =>
                        cellSb.Append(cell)).Append(Environment.NewLine))
                .ToString();

        public IEnumerable<World> Ticks()
        {
            yield return this;
            var nextState = ImmutableArray.CreateRange(Cells.AsParallel().Select((row, outerInd) => ImmutableArray.CreateRange(
                row.AsParallel().Select((_, innerInd) => UpdateCell(outerInd, innerInd)))));

            foreach (var state in WithCells(nextState).Ticks())
            {
                yield return state;
            }
        }

        protected Cell UpdateCell(int outerIndex, int innerIndex)
        {
            var alive = Neighbours.GetNeighbours(Cells, outerIndex, innerIndex).Count(n => n.IsAlive);

            return new Match<Cell, Cell>(
                    (c => !c.IsAlive && alive == 3, _ => new Cell(true, 1)),
                    (_ => alive < 2 || alive > 3, _ => new Cell(false, 0)),
                    (_ => true, c => new Cell(c.IsAlive, c.LifeTime + 1)))
                .MatchFirst(Cells[outerIndex][innerIndex]);
        }
    }
}