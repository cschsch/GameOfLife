using GameOfLife.Helpers;

namespace GameOfLife.Entities.Environmental
{
    public class EnvironmentalWorldData : BaseWorldData<EnvironmentalCell, EnvironmentalCellGrid>
    {
        public double Temperature { get; set; }
        public Density HerbivoreDensity { get; set; }
    }
}