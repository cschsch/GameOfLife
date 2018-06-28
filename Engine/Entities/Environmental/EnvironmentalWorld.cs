using System.Collections.Generic;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalWorld : BaseWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public override IEnumerable<EnvironmentalWorld> Ticks() => WorldGenerator.Ticks(this);
    }
}