using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Renderer;

namespace GameOfLife.Core
{
    public class Game
    {
        private IRenderer Renderer { get; }
        private IAnalyzeResults ResultAnalyzer { get; }

        public Game(IRenderer renderer, IAnalyzeResults resultAnalyzer)
        {
            Renderer = renderer;
            ResultAnalyzer = resultAnalyzer;
        }

        public void Init() => Renderer.GenerationWatch.Start();

        public void GameLoop(World world, int sleep)
        {
            var nextWorld = PrintGenerations(world, sleep);
            ResultAnalyzer.PrintResults();
            GameLoop(nextWorld, sleep);
        }

        private World PrintGenerations(World lastWorld, int sleepInMs)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(ResultAnalyzer.PrintInterval))
            {
                Renderer.PrintUi(tick.Data);
                ResultAnalyzer.CollectData(tick.Data);
                tickToReturn = tick;
                System.Threading.Thread.Sleep(sleepInMs);
            }

            return tickToReturn;
        }
    }
}