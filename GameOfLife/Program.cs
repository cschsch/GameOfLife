using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

using CommandLine;

using Engine.Entities.Environmental;
using Engine.Entities.Standard;
using Engine.Helpers;
using Engine.Strategies.SeasonStrategies;

using GameOfLife.Entities;
using GameOfLife.Helpers;
using GameOfLife.Mappers;

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

        private static GameVariables MapArgs(CommandOptions options)
        {
            if (options.GameType == GameType.None)
            {
                Console.WriteLine($"Couldn't resolve {nameof(options.GameType)}, stopping execution.");
                return new GameVariables {GameType = GameType.None};
            }

            var mapper = new EngineTypeMapper();

            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            var constructorArg = new Match<CommandOptions, Either<string, int>>(
                (opt => opt.FigureName.Length > 0, opt => Either.CreateLeft<string, int>(figures[opt.FigureName.ToUpper()])),
                (opt => opt.FilePath.Length > 0, opt => Either.CreateLeft<string, int>(File.ReadAllText(opt.FilePath))),
                (_ => true, opt => Either.CreateRight<string, int>(opt.Size))).MatchFirst(options);
            var grid = mapper.CellGridTypeMap[options.GameType];

            var worldDataBuilder = mapper.WorldDataTypeMap[options.GameType]();
            var data = worldDataBuilder
                .With(wd => wd.Grid, wd => wd.Grid, grid(constructorArg))
                .With(Maybe<Expression<Func<StandardWorldData, double>>>.Nothing, Maybe.Just<Expression<Func<EnvironmentalWorldData, double>>>(wd => wd.Temperature), options.Temperature)
                .With(Maybe<Expression<Func<StandardWorldData, SeasonalTime>>>.Nothing, Maybe.Just<Expression<Func<EnvironmentalWorldData, SeasonalTime>>>(wd => wd.Season), SeasonalTime.Spring)
                .Create();

            var neighbourFinder = options.Closed
                ? mapper.ClosedNeighbourFinderMap[options.GameType]
                : mapper.OpenNeighbourFinderMap[options.GameType];
            var cellCalculator = mapper.CellCalculatorMap[options.GameType];
            var worldGenerator = mapper.WorldGeneratorMap[options.GameType];
            var seasonCalculator = options.Seasons ? (ICalculateSeason) new StandardSeasonCalculator() : new NoSeasonCalculator();

            var worldBuilder = mapper.WorldTypeMap[options.GameType]();
            var world = worldBuilder
                .With(w => w.Data, w => w.Data, data)
                .With(w => w.NeighbourFinder, w => w.NeighbourFinder, neighbourFinder())
                .With(w => w.CellCalculator, w => w.CellCalculator, cellCalculator())
                .With(w => w.WorldGenerator, w => w.WorldGenerator, worldGenerator())
                .With(Maybe<Expression<Func<StandardWorld, ICalculateSeason>>>.Nothing, Maybe.Just<Expression<Func<EnvironmentalWorld, ICalculateSeason>>>(w => w.SeasonCalculator), seasonCalculator)
                .Create();

            return new GameVariables
            {
                World = world,
                GameType = options.GameType,
                ThreadSleep = options.ThreadSleep,
                PrintInterval = options.PrintInterval,
                PrintFile = options.PrintFile
            };
        }

        private static void StartGame(GameVariables variables)
        {
            if (variables.GameType == GameType.None) return;

            var mapper = new GeneralTypeMapper();

            var renderer = mapper.ConsoleRendererMap[variables.GameType];
            var resultAnalyzer = mapper.ResultAnalyzerMap[variables.GameType];
            var game = mapper.GameMapper[variables.GameType];

            game(renderer(), resultAnalyzer(variables.PrintInterval, Maybe.Just(variables.PrintFile))).Switch(
                left =>
                {
                    left.Init();
                    left.GameLoop(variables.World.Left(), variables.ThreadSleep);
                }, right =>
                {
                    right.Init();
                    right.GameLoop(variables.World.Right(), variables.ThreadSleep);
                });
        }
    }
}
