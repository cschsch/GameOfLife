using Engine.Entities.Environmental;
using Engine.Entities.Standard;
using Tp.Core;

namespace GameOfLife.Entities
{
    internal struct GameVariables
    {
        public Either<StandardWorld, EnvironmentalWorld> World { get; set; }
        public GameType GameType { get; set; }
        public int ThreadSleep { get; set; }
        public int PrintInterval { get; set; }
        public string PrintFile { get; set; }
    }
}