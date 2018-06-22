using System.Collections.Generic;
using GameOfLife.Core.Worlds;

namespace GameOfLife.Core.Enumerators
{
    public interface IEnumerateWorld
    {
        IEnumerable<World> Ticks(World world);
    }
}