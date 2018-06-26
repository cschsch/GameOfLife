using System;
using System.IO;
using System.Linq;
using CommandLine;
using GameOfLife.Core;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Entities;
using GameOfLife.Entities.Builder;
using GameOfLife.Helpers;
using GameOfLife.Renderer;
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
            var grid = new Match<CommandOptions, CellGrid>(
                (opt => opt.FigureName.Length > 0, opt => new CellGrid(figures[opt.FigureName.ToUpper()])),
                (opt => opt.FilePath.Length > 0, opt => new CellGrid(File.ReadAllText(opt.FilePath))),
                (_ => true, opt => new CellGrid(opt.Size))).MatchFirst(opts);

            var data = new WorldDataBuilder()
                .WithGrid(grid)
                .WithTemperature(opts.Temperature)
                .Create();

            var neighbourFinder = opts.Closed ? new ClosedNeighbourFinder() : (IFindNeighbours) new OpenNeighbourFinder();
            var cellCalculator = new EnvironmentalCellCalculator(new Random());
            var generator = new StandardWorldGenerator();
            var world = new WorldBuilder()
                .WithData(data)
                .WithNeighbourFinder(neighbourFinder)
                .WithCellCalculator(cellCalculator)
                .WithGenerator(generator)
                .Create();

            return new GameVariables
            {
                World = world,
                ThreadSleep = opts.ThreadSleep,
            };
        }

        private static void StartGame(GameVariables variables)
        {
            var renderer = new ConsoleRenderer(variables.World.Data.Grid.Cells.Count);
            var game = new Game(renderer);
            game.Init();
            game.GameLoop(variables.World, variables.ThreadSleep);
        }
    }

    internal struct GameVariables
    {
        public World World { get; set; }
        public int ThreadSleep { get; set; }
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
    }
}
