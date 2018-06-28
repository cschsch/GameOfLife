using System.Collections.Generic;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;

namespace GameOfLife.Entities
{
    public abstract class BaseWorld<TCell, TCellGrid, TWorldData, TWorld>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
        where TWorld : BaseWorld<TCell, TCellGrid, TWorldData, TWorld>
    {
        public TWorldData Data { get; set; }
        public IFindNeighbours<TCell, TCellGrid> NeighbourFinder { get; set; }
        public ICalculateCell<TCell, TCellGrid, TWorldData> CellCalculator { get; set; }
        public IGenerateWorld<TCell, TCellGrid, TWorldData, TWorld> WorldGenerator { get; set; }
        public abstract IEnumerable<TWorld> Ticks();
    }
}