using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Renderer;

namespace GameOfLife.Core
{
    public class Game
    {
        private IRenderer Renderer { get; }

        public Game(IRenderer renderer) => Renderer = renderer;

        public void Init() => Renderer.GenerationWatch.Start();

        public void GameLoop(World world, int sleep)
        {
            var nextWorld = PrintOneHundredGenerations(world, sleep);
            GameLoop(nextWorld, sleep);
        }

        private World PrintOneHundredGenerations(World lastWorld, int sleepInMs)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(100))
            {
                Renderer.PrintUi(tick.Data);
                tickToReturn = tick;
                System.Threading.Thread.Sleep(sleepInMs);
            }

            return tickToReturn;
        }
    }
}