namespace GameOfLife.Core
{
    public class Cell
    {
        public bool IsAlive { get; }
        public int LifeTime { get; }
        
        public Cell(bool isAlive, int lifeTime)
        {
            IsAlive = isAlive;
            LifeTime = lifeTime;
        }
    }
}