using System.Collections.Generic;
using System.Collections.Immutable;

namespace GameOfLife.Core
{
    public interface IGetNeighbours
    {
        IEnumerable<Cell> GetNeighbours(ImmutableArray<ImmutableArray<Cell>> cells, int outerIndex, int innerIndex);
    }
}