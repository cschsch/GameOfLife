namespace GameOfLife.Entities
{
    public abstract class BaseCell
    {
        public bool IsAlive { get; set; }
        public int LifeTime { get; set; }

        public static char Alive => 'X';
        public static char DeadOut => ' ';
        public static char DeadIn => '-';
    }
}