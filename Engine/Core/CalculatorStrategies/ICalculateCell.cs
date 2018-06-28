using System.Collections.Generic;
using Engine.Entities;

namespace Engine.Core.CalculatorStrategies
{
    /// <summary>
    /// Needed to calculate the next state of each cell
    /// </summary>
    /// <typeparam name="TCell">Type of cell</typeparam>
    /// <typeparam name="TCellGrid">Type of cell grid</typeparam>
    /// <typeparam name="TWorldData">Type of world data</typeparam>
    public interface ICalculateCell<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        /// <summary>
        /// Returns the next state of <paramref name="cell"/>, depending on <paramref name="neighbours"/> and <paramref name="data"/>
        /// </summary>
        /// <param name="cell">Cell to calculate next state of</param>
        /// <param name="neighbours">The adjacent cells of <paramref name="cell"/></param>
        /// <param name="data">Data for additional information concerning calculation</param>
        /// <returns>Next state of <paramref name="cell"/></returns>
        TCell CalculateCell(TCell cell, IEnumerable<TCell> neighbours, TWorldData data);
    }
}