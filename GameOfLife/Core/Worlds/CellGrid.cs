using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core.Worlds
{
    public class CellGrid
    {
        public IReadOnlyList<IReadOnlyList<Cell>> Cells { get; }

        public CellGrid(IReadOnlyList<IReadOnlyList<Cell>> cells) => Cells = cells;

        public CellGrid(string cellRep) => Cells = cellRep.Split(Environment.NewLine).Select(row =>
            row.Select(c => c == ' ' ? new Cell { IsAlive = false, LifeTime = 0 } : new Cell { IsAlive = true, LifeTime = 1 }).ToArray()).ToArray();

        public CellGrid(int size, Func<Random> randomFactory)
        {
            var random = randomFactory();
            Cell GenerateCell() => new Cell { IsAlive = random.NextDouble() >= 0.5, LifeTime = 1 };

            Cells = EnumerablePrelude.Repeat(GenerateCell, size * size).Partition(size).Select(row => row.ToArray()).ToArray();
        }

        public CellGrid(int size) : this(size, () => new Random()) { }
    }
}