using System.Linq;
using GameOfLife.Renderer;

namespace GameOfLife.Core
{
    public class Game
    {
        private IRenderer Renderer { get; }
        
        public Game(IRenderer renderer) => Renderer = renderer;

        public void Init() => Renderer.GenerationWatch.Start();

        public void GameLoop((World world, int sleep) args, int generation = 1)
        {
            var nextWorld = PrintOneHundredGenerations(args.world, args.sleep, generation);
            GameLoop((nextWorld, args.sleep), generation + 100);
        }

        private World PrintOneHundredGenerations(World lastWorld, int sleepInMs, int generation)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(100))
            {
                Renderer.PrintTick(tick);
                Renderer.PrintGeneration(generation);
                generation++;
                tickToReturn = tick;
                System.Threading.Thread.Sleep(sleepInMs);
            }

            return tickToReturn;
        }
    }
}