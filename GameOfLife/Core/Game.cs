using System.Linq;
using GameOfLife.Renderer;

namespace GameOfLife.Core
{
    public class Game
    {
        private IRenderer Renderer { get; }

        public Game(int worldSize, (char alive, char dead) cellRep) : this(new ConsoleRenderer(worldSize, cellRep)) { }
        public Game(IRenderer renderer) => Renderer = renderer;

        public void Init() => Renderer.GenerationWatch.Start();

        public void GameLoop((IWorld world, int sleep) args, int generation = 1)
        {
            var nextWorld = PrintOneHundredGenerations(args.world, args.sleep, generation);
            GameLoop((nextWorld, args.sleep), generation + 100);
        }

        private IWorld PrintOneHundredGenerations(IWorld lastWorld, int sleepInMs, int generation)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(100))
            {
                Renderer.PrintTick(tick);
                generation++;
                Renderer.PrintGeneration(generation);
                tickToReturn = tick;
                System.Threading.Thread.Sleep(sleepInMs);
            }

            return tickToReturn;
        }
    }
}