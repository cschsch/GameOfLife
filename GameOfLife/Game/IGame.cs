namespace GameOfLife.Game
{
    public interface IGame
    {
        void Init();
        void GameLoop(object world, int sleep);
    }
}