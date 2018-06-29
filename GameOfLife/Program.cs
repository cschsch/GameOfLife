using System;
using System.IO;
using System.Linq;
using CommandLine;
using Engine.Core.CalculatorStrategies;
using Engine.Core.GeneratorStrategies;
using Engine.Core.NeighbourStrategies;
using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builder;
using Engine.Helpers;
using GameOfLife.Game;
using GameOfLife.ResultAnalyzer;
using Graphics.Renderer;
using Tp.Core;

namespace GameOfLife
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var worldOrError = Parser.Default.ParseArguments<CommandOptions>(args).MapResult(
                opts => Either.CreateLeft<GameVariables, string>(MapArgs(opts)),
                err => Either.CreateRight<GameVariables, string>(string.Join(", ", err)));

            worldOrError.Switch(StartGame, Console.Write);
        }

        private static GameVariables MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            var grid = new Match<CommandOptions, EnvironmentalCellGrid>(
                (opt => opt.FigureName.Length > 0, opt => new EnvironmentalCellGrid(figures[opt.FigureName.ToUpper()])),
                (opt => opt.FilePath.Length > 0, opt => new EnvironmentalCellGrid(File.ReadAllText(opt.FilePath))),
                (_ => true, opt => new EnvironmentalCellGrid(opt.Size))).MatchFirst(opts);

            var data = new EnvironmentalWorldDataBuilder()
                .With(wd => wd.Grid, grid)
                .With(wd => wd.Temperature, opts.Temperature)
                .Create();

            var neighbourFinder = opts.Closed
                ? new ClosedNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>()
                : (IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>) new OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>();
            var cellCalculator = new EnvironmentalCellCalculator(new Random());
            var generator = new EnvironmentalWorldGenerator();
            var world = new EnvironmentalWorldBuilder()
                .With(w => w.Data, data)
                .With(w => w.NeighbourFinder, neighbourFinder)
                .With(w => w.CellCalculator, cellCalculator)
                .With(w => w.WorldGenerator, generator)
                .Create();

            return new GameVariables
            {
                World = world,
                ThreadSleep = opts.ThreadSleep,
                PrintInterval = opts.PrintInterval,
                PrintFile = opts.PrintFile
            };
        }

        private static void StartGame(GameVariables variables)
        {
            var renderer = new EnvironmentalConsoleRenderer(variables.World.Data.Grid.Cells.Count);
            var resultAnalyzer = new EnvironmentalResultAnalyzer(variables.PrintInterval, variables.PrintFile);
            var game = new EnvironmentalGame(renderer, resultAnalyzer);
            game.Init();
            game.GameLoop(variables.World, variables.ThreadSleep);
        }
    }

    internal struct GameVariables
    {
        public EnvironmentalWorld World { get; set; }
        public int ThreadSleep { get; set; }
        public int PrintInterval { get; set; }
        public string PrintFile { get; set; }
    }

    internal class CommandOptions
    {
        [Option('s', "size", Default = 69)]
        public int Size { get; set; }

        [Option('f', "figure", Default = "")]
        public string FigureName { get; set; }

        [Option("file", Default = "")]
        public string FilePath { get; set; }

        [Option('t', "thread_sleep")]
        public int ThreadSleep { get; set; }

        [Option('c', "closed")]
        public bool Closed { get; set; }

        [Option("temperature")]
        public int Temperature { get; set; }

        [Option('i', "print_interval", Default = 100)]
        public int PrintInterval { get; set; }

        [Option('p', "print_file", Default = @"analyzation\01.txt")]
        public string PrintFile { get; set; }
    }
}
