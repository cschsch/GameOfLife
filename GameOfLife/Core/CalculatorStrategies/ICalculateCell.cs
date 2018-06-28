using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.CalculatorStrategies
{
    public interface ICalculateCell<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        TCell CalculateCell(TCell cell, IEnumerable<TCell> neighbours, TWorldData data);
    }
}