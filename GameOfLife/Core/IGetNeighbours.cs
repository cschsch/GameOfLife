using System.Collections.Generic;
using System.Collections.Immutable;

namespace GameOfLife.Core
{
    public interface IGetNeighbours
    {
        IEnumerable<Cell> GetNeighbours(ImmutableArray<ImmutableArray<Cell>> cells, Cell cell, int outerIndex, int innerIndex);
    }
}