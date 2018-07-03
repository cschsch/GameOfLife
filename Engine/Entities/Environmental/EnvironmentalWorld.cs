using System.Collections.Generic;
using Engine.Strategies.SeasonStrategies;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalWorld : BaseWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>
    {
        public ICalculateSeason SeasonCalculator { get; set; }

        public override IEnumerable<EnvironmentalWorld> Ticks() => WorldGenerator.Ticks(this);
    }
}