using System.Collections.Generic;
using GameOfLife.Entities;

namespace GameOfLife.Core.GeneratorStrategies
{
    public interface IGenerateWorld
    {
        IEnumerable<World> Ticks(World world);
    }
}