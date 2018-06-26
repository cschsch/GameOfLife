namespace GameOfLife.Entities
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public int LifeTime { get; set; }
        public DietaryRestrictions Diet { get; set; }

        public Cell() { }

        public Cell(Cell other)
        {
            IsAlive = other.IsAlive;
            LifeTime = other.LifeTime;
            Diet = other.Diet;
        }

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