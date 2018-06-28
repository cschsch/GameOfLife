using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Core.NeighbourStrategies
{
    public class OpenNeighbourFinder<TCell, TCellGrid> : IFindNeighbours<TCell, TCellGrid>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
    {
        public IEnumerable<TCell> FindNeighbours(TCellGrid cells, int outerIndex, int innerIndex)
        {
            var size = cells.Cells.Count;
            return cells.Cells.GetValues(Enumerable.Range(outerIndex - 1, 3).Select(ind => (ind + size) % size))
                .SelectMany(row => row.GetValues(Enumerable.Range(innerIndex - 1, 3).Select(ind => (ind + size) % size)))
                .Except(new[] {cells.Cells[outerIndex][innerIndex]});
        }
    }
}