using System.Collections.Generic;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Core.CalculatorStrategies;

namespace GameOfLife.Entities
{
    public class World
    {
        public WorldData Data { get; set; }
        public IFindNeighbours NeighbourFinder { get; set; }
        public ICalculateCell CellCalculator { get; set; }
        public IGenerateWorld Generator { get; set; }

        public IEnumerable<World> Ticks() => Generator.Ticks(this);
    }
}