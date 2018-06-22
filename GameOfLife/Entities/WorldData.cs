namespace GameOfLife.Entities
{
    public class WorldData
    {
        public CellGrid Grid { get; set; }
        public int Temperature { get; set; }
        public double HerbivoreDensity { get; set; }
    }
}