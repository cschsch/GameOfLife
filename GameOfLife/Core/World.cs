using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Core.Calculators;
using GameOfLife.Core.Neighbours;
using GameOfLife.Helpers;
using Tp.Core;

namespace GameOfLife.Core
{
    public class World
    {
        public IReadOnlyList<IReadOnlyList<Cell>> Cells { get; }
        public IFindNeighbours NeighbourFinder { get; }
        public ICalculateCell CellCalculator { get; }

        private World(Maybe<IReadOnlyList<IReadOnlyList<Cell>>> cells, Maybe<IFindNeighbours> neighbourFinder, Maybe<ICalculateCell> cellCalculator)
        {
            Cells = cells.GetOrElse(() => new Cell[0][]);
            NeighbourFinder = neighbourFinder.GetOrElse(() => new OpenNeighbourFinder());
            CellCalculator = cellCalculator.GetOrElse(() => new StandardCellCalculator());
        }

        public World() { }
        public World WithNeighbourFinder(IFindNeighbours neighbourFinder) => new World(Maybe.Return(Cells), Maybe.Return(neighbourFinder), Maybe.Return(CellCalculator));
        public World WithCells(IReadOnlyList<IReadOnlyList<Cell>> cells) => new World(Maybe.Return(cells), Maybe.Return(NeighbourFinder), Maybe.Return(CellCalculator));
        public World WithCellCalculator(ICalculateCell cellCalculator) => new World(Maybe.Return(Cells), Maybe.Return(NeighbourFinder), Maybe.Return(cellCalculator));

        public World(string cellRep) =>
            Cells = cellRep.Split(Environment.NewLine).Select(row =>
                        row.Select(c => c == ' ' ? new Cell{IsAlive = false, LifeTime = 0} : new Cell{IsAlive = true, LifeTime = 1}).ToArray()).ToArray();

        public World(int size) : this(size, () => new Random()) { }

        public World(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new Cell{IsAlive = random.NextDouble() >= 0.5, LifeTime = 1};

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }

        public IEnumerable<World> Ticks()
        {
            yield return this;

            var nextState = Cells.AsParallel().Select((row, outerInd) => 
                row.AsParallel().Select((_, innerInd) =>
                    CellCalculator.CalculateCell(Cells[outerInd][innerInd],
                        NeighbourFinder.FindNeighbours(Cells, outerInd, innerInd))).ToArray()).ToArray();

            foreach (var state in WithCells(nextState).Ticks())
            {
                yield return state;
            }
        }
    }
}