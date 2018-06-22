using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Helpers;

namespace GameOfLife.Core.NeighbourStrategies
{
    public class OpenNeighbourFinder : IFindNeighbours
    {
        public IEnumerable<Cell> FindNeighbours(IReadOnlyList<IReadOnlyList<Cell>> cells, int outerIndex, int innerIndex)
        {
            var size = cells.Count;
            return cells.GetValues(Enumerable.Range(outerIndex - 1, 3).Select(ind => (ind + size) % size))
                .SelectMany(row => row.GetValues(Enumerable.Range(innerIndex - 1, 3).Select(ind => (ind + size) % size)))
                .Except(new[] {cells[outerIndex][innerIndex]});
        }
    }
}