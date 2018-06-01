namespace GameOfLife.Core
{
    public class Cell
    {
        public bool IsAlive { get; }
        public int LifeTime { get; }

        public static string Alive => "X";

        public Cell(bool isAlive, int lifeTime)
        {
            IsAlive = isAlive;
            LifeTime = lifeTime;
        }

        public override string ToString() => IsAlive ? Alive : " ";
    }
}