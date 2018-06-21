using System.Collections.Generic;

namespace GameOfLife.Core.Neighbours
{
    public interface IFindNeighbours
    {
        IEnumerable<Cell> FindNeighbours(IReadOnlyList<IReadOnlyList<Cell>> cells, int outerIndex, int innerIndex);
    }
}