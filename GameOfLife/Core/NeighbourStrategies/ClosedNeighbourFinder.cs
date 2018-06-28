using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Core.NeighbourStrategies
{
    public class ClosedNeighbourFinder<TCell> : IFindNeighbours<TCell> where TCell : BaseCell
    {
        public IEnumerable<TCell> FindNeighbours(IReadOnlyList<IReadOnlyList<TCell>> cells, int outerIndex, int innerIndex) =>
            cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3))).Except(new[] {cells[outerIndex][innerIndex]});
    }
}