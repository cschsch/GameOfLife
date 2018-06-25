using GameOfLife.Helpers;

namespace GameOfLife.Entities
{
    public class WorldData
    {
        public int Generation { get; set; }
        public CellGrid Grid { get; set; }
        public double Temperature { get; set; }
        public Density HerbivoreDensity { get; set; }
    }
}