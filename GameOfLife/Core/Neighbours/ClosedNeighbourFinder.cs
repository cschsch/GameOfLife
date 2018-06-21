using System.Collections.Generic;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core.Neighbours
{
    public class ClosedNeighbourFinder : IFindNeighbours
    {
        public IEnumerable<Cell> FindNeighbours(IReadOnlyList<IReadOnlyList<Cell>> cells, int outerIndex, int innerIndex) =>
            cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3))).Except(new[] {cells[outerIndex][innerIndex]});
    }
}