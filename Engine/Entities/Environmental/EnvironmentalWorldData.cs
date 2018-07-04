using Engine.Helpers;

namespace Engine.Entities.Environmental
{
    public class EnvironmentalWorldData : BaseWorldData<EnvironmentalCell, EnvironmentalCellGrid>
    {
        public double Temperature { get; set; }
        public Density HerbivoreDensity { get; set; }

        [Dependency]
        public SeasonalTime Season { get; set; }
    }
}