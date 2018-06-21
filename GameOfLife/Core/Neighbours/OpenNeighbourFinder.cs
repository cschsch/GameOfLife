using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using GameOfLife.Helpers;

namespace GameOfLife.Core.Neighbours
{
    public class OpenNeighbourFinder : IFindNeighbours
    {
        public IEnumerable<Cell> FindNeighbours(ImmutableArray<ImmutableArray<Cell>> cells, int outerIndex, int innerIndex)
        {
            var size = cells.Length;
            return cells.GetValues(Enumerable.Range(outerIndex - 1, 3).Select(ind => (ind + size) % size))
                .SelectMany(row => row.GetValues(Enumerable.Range(innerIndex - 1, 3).Select(ind => (ind + size) % size)))
                .Except(new[] {cells[outerIndex][innerIndex]});
        }
    }
}