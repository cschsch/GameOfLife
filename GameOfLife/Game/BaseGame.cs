﻿using System.Linq;
using System.Threading.Tasks;
using Engine.Entities;

using Graphics.Console;

using ResultAnalyzer;

namespace GameOfLife.Game
{
    public abstract class BaseGame<TCell, TCellGrid, TWorldData, TWorld> : IGame
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
        where TWorld : BaseWorld<TCell, TCellGrid, TWorldData, TWorld>
    {
        private IRenderer<TCell, TCellGrid, TWorldData> Renderer { get; }
        private IAnalyzeResults<TCell, TCellGrid, TWorldData> ResultAnalyzer { get; }

        protected BaseGame(IRenderer<TCell, TCellGrid, TWorldData> renderer, IAnalyzeResults<TCell, TCellGrid, TWorldData> resultAnalyzer)
        {
            Renderer = renderer;
            ResultAnalyzer = resultAnalyzer;
        }

        public void Init() => Renderer.GenerationWatch.Start();

        public void GameLoop(object world, int sleep)
        {
            var nextWorld = PrintGenerations((TWorld) world, sleep);
            ResultAnalyzer.PrintResultsAsync();
            GameLoop(nextWorld, sleep);
        }

        private TWorld PrintGenerations(TWorld lastWorld, int sleepInMs)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(ResultAnalyzer.PrintInterval))
            {
                Parallel.Invoke(
                    () => Renderer.PrintUi(tick.Data),
                    () => ResultAnalyzer.CollectData(tick.Data));
                tickToReturn = tick;
                System.Threading.Thread.Sleep(sleepInMs);
            }

            return tickToReturn;
        }
    }
}