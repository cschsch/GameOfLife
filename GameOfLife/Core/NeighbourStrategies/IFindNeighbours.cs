using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.NeighbourStrategies
{
    public interface IFindNeighbours<TCell>
        where TCell : BaseCell
    {
        IEnumerable<TCell> FindNeighbours(IReadOnlyList<IReadOnlyList<TCell>> cells, int outerIndex, int innerIndex);
    }
}