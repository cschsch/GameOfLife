using System.Collections.Generic;
using System.Collections.Immutable;

namespace GameOfLife.Core.Neighbours
{
    public interface IFindNeighbours
    {
        IEnumerable<Cell> FindNeighbours(ImmutableArray<ImmutableArray<Cell>> cells, int outerIndex, int innerIndex);
    }
}