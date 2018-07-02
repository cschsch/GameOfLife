using System.Collections.Generic;
using Engine.Entities;

namespace Engine.Strategies.GeneratorStrategies
{
    /// <summary>
    /// Needed to generate the next world from a given one
    /// </summary>
    /// <typeparam name="TCell">Type of cell</typeparam>
    /// <typeparam name="TCellGrid">Type of cell grid</typeparam>
    /// <typeparam name="TWorldData">Type of world data</typeparam>
    /// <typeparam name="TWorld">Type of world</typeparam>
    public interface IGenerateWorld<TCell, TCellGrid, TWorldData, TWorld>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
        where TWorld : BaseWorld<TCell, TCellGrid, TWorldData, TWorld>
    {
        /// <summary>
        /// Generates a sequence of world starting from <paramref name="world"/>
        /// </summary>
        /// <param name="world">Initial state</param>
        /// <returns>Sequence of next states of <paramref name="world"/></returns>
        IEnumerable<TWorld> Ticks(TWorld world);
    }
}