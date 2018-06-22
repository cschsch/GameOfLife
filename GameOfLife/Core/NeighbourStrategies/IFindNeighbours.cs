using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.NeighbourStrategies
{
    public interface IFindNeighbours
    {
        IEnumerable<Cell> FindNeighbours(IReadOnlyList<IReadOnlyList<Cell>> cells, int outerIndex, int innerIndex);
    }
}