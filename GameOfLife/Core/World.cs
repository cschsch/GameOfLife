using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Core.Calculators;
using GameOfLife.Core.Neighbours;
using GameOfLife.Helpers;
using Tp.Core;

namespace GameOfLife.Core
{
    public class World
    {
        public ImmutableArray<ImmutableArray<Cell>> Cells { get; }
        public IFindNeighbours NeighbourFinder { get; }
        public ICalculateCell CellCalculator { get; }

        private World(Maybe<ImmutableArray<ImmutableArray<Cell>>> cells, Maybe<IFindNeighbours> neighbourFinder, Maybe<ICalculateCell> cellCalculator)
        {
            Cells = cells.GetOrElse(() => ImmutableArray<ImmutableArray<Cell>>.Empty);
            NeighbourFinder = neighbourFinder.GetOrElse(() => new OpenNeighbourFinder());
            CellCalculator = cellCalculator.GetOrElse(() => new StandardCellCalculator());
        }

        public World() { }
        public World WithNeighbourFinder(IFindNeighbours neighbourFinder) => new World(Maybe.Return(Cells), Maybe.Return(neighbourFinder), Maybe.Return(CellCalculator));
        public World WithCells(ImmutableArray<ImmutableArray<Cell>> cells) => new World(Maybe.Return(cells), Maybe.Return(NeighbourFinder), Maybe.Return(CellCalculator));
        public World WithCellCalculator(ICalculateCell cellCalculator) => new World(Maybe.Return(Cells), Maybe.Return(NeighbourFinder), Maybe.Return(cellCalculator));

        public World(string cellRep) =>
            Cells = ImmutableArray.CreateRange(cellRep.Split(Environment.NewLine).Select(row =>
                    ImmutableArray.CreateRange(row.Select(c => c == ' ' ? new Cell(false, 0) : new Cell(true, 1)))));

        public World(int size) : this(size, () => new Random()) { }

        public World(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new Cell(random.NextDouble() >= 0.5, 1);

            Cells = ImmutableArray.CreateRange(
                EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(ImmutableArray.CreateRange));
        }

        public IEnumerable<World> Ticks()
        {
            yield return this;

            var nextState = ImmutableArray.CreateRange(Cells.AsParallel().Select((row, outerInd) => ImmutableArray.CreateRange(
                row.AsParallel().Select((_, innerInd) =>
                    CellCalculator.CalculateCell(Cells[outerInd][innerInd],
                        NeighbourFinder.FindNeighbours(Cells, outerInd, innerInd))))));

            foreach (var state in WithCells(nextState).Ticks())
            {
                yield return state;
            }
        }
    }
}