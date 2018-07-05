using System.Collections.Generic;
using System.Linq;

using Engine.Entities;
using Engine.Helpers.Functions;

namespace Engine.Strategies.NeighbourStrategies
{
    public class ClosedNeighbourFinder<TCell, TCellGrid> : IFindNeighbours<TCell, TCellGrid>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
    {
        public IEnumerable<TCell> FindNeighbours(TCellGrid cells, int outerIndex, int innerIndex) =>
            cells.Cells.GetValuesSafe(Enumerable.Range(outerIndex - 1, 3))
                .SelectMany(row => row.GetValuesSafe(Enumerable.Range(innerIndex - 1, 3)))
                .Skip(cells.Cells[outerIndex][innerIndex], 1);
    }
}