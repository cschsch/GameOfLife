using System.Collections.Generic;
using Engine.Entities;

namespace Engine.Core.NeighbourStrategies
{
    /// <summary>
    /// Needed to find the cells adjacent to a given one
    /// </summary>
    /// <typeparam name="TCell">Type of cell</typeparam>
    /// <typeparam name="TCellGrid">Type of cell grid</typeparam>
    public interface IFindNeighbours<out TCell, in TCellGrid>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
    {
        /// <summary>
        /// Finds the (usually 8) adjacent neighbours of a given cell
        /// </summary>
        /// <param name="cells">Complete grid of cells</param>
        /// <param name="outerIndex">Row index of cell</param>
        /// <param name="innerIndex">Inner index of cell</param>
        /// <returns>Sequence of neighbours</returns>
        IEnumerable<TCell> FindNeighbours(TCellGrid cells, int outerIndex, int innerIndex);
    }
}