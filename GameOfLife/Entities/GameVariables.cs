using GameOfLife.Game;

namespace GameOfLife.Entities
{
    internal struct GameVariables
    {
        public object World { get; set; }
        public IGame Game { get; set; }
        public int ThreadSleep { get; set; }
    }
}