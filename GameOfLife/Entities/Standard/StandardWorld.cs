using System.Collections.Generic;

namespace GameOfLife.Entities.Standard
{
    public class StandardWorld : BaseWorld<StandardCell, StandardCellGrid, StandardWorldData, StandardWorld>
    {
        public override IEnumerable<StandardWorld> Ticks() => WorldGenerator.Ticks(this);
    }
}