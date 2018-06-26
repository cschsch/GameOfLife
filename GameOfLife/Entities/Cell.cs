namespace GameOfLife.Entities
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int LifeTime { get; set; }
        public DietaryRestrictions Diet { get; set; }

        public static char DeadOut = ' ';
        public static char DeadIn => '-';
        public static char Carnivore => 'X';
        public static char Herbivore => 'O';
    }

    public enum DietaryRestrictions
    {
        Carnivore, Herbivore
    }
}