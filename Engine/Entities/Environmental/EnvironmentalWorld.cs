using System.Collections.Generic;

using Engine.Helpers;
using Engine.Strategies.SeasonStrategies;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalWorld : BaseWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        [Dependency]
        public ICalculateSeason SeasonCalculator { get; set; }

        public override IEnumerable<EnvironmentalWorld> Ticks() => WorldGenerator.Ticks(this);
    }
}