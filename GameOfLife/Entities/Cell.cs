namespace GameOfLife.Entities
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int LifeTime { get; set; }
        public DietaryRestrictions Diet { get; set; }
    }

    public enum DietaryRestrictions
    {
        Carnivore, Herbivore
    }
}