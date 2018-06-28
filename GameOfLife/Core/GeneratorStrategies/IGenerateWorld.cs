using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.GeneratorStrategies
{
    public interface IGenerateWorld<TCell, TCellGrid, TWorldData, TWorld>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
        where TWorld : BaseWorld<TCell, TCellGrid, TWorldData, TWorld>
    {
        IEnumerable<TWorld> Ticks(TWorld world);
    }
}