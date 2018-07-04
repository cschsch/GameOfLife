using System;
using System.IO;
using System.Linq;

using Engine.Entities;
using Engine.Entities.Environmental;
using Engine.Entities.Standard;
using Engine.Helpers;
using Engine.Strategies.CalculatorStrategies;
using Engine.Strategies.GeneratorStrategies;
using Engine.Strategies.NeighbourStrategies;
using Engine.Strategies.SeasonStrategies;

using GameOfLife.Entities;
using GameOfLife.Entities.Enums;
using GameOfLife.Game;

using Graphics.Console;

using Ninject.Modules;

using ResultAnalyzer;

using Tp.Core;

namespace GameOfLife
{
    internal class EngineModule : NinjectModule
    {
        private CommandOptions Options { get; }

        public EngineModule(CommandOptions options) => Options = options;

        public override void Load()
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string)p.GetValue(p));
            var cellRepOrSize = new Match<CommandOptions, Either<string, int>>(
                (opt => opt.FigureName.Length > 0, opt => Either.CreateLeft<string, int>(figures[opt.FigureName.ToUpper()])),
                (opt => opt.FilePath.Length > 0, opt => Either.CreateLeft<string, int>(File.ReadAllText(opt.FilePath))),
                (_ => true, opt => Either.CreateRight<string, int>(opt.Size))).MatchFirst(Options);

            switch (Options.GameType)
            {
                case GameType.None:
                    throw new InvalidOperationException(nameof(GameType));
                case GameType.Standard:
                    LoadStandard(cellRepOrSize, Options);
                    break;
                case GameType.Environmental:
                    LoadEnvironmental(cellRepOrSize, Options);
                    break;
            }
        }

        private void LoadStandard(Either<string, int> cellGridConstructorArgument, CommandOptions options)
        {
            cellGridConstructorArgument.Switch(
                cellRep => Kernel.Bind<StandardCellGrid>().ToSelf().WithConstructorArgument(cellRep),
                size => Kernel.Bind<StandardCellGrid>().ToSelf().WithConstructorArgument(size));

            Kernel.Bind<StandardWorldData>().ToSelf();
            Kernel.Bind(typeof(ICalculateCell<,,>)).To<StandardCellCalculator>();
            Kernel.Bind(typeof(IGenerateWorld<,,,>)).To<StandardWorldGenerator>();

            switch (options.NeighbourFinder)
            {
                case NeighbourFinderType.None:
                    throw new InvalidOperationException(nameof(NeighbourFinderType));
                case NeighbourFinderType.Closed:
                    Kernel.Bind(typeof(IFindNeighbours<,>)).To<ClosedNeighbourFinder<StandardCell, StandardCellGrid>>();
                    break;
                case NeighbourFinderType.Open:
                    Kernel.Bind(typeof(IFindNeighbours<,>)).To<OpenNeighbourFinder<StandardCell, StandardCellGrid>>();
                    break;
            }

            Kernel.Bind(typeof(BaseWorld<,,,>)).To<StandardWorld>();
            Kernel.Bind(typeof(IRenderer<,,>)).To<StandardConsoleRenderer>();
            Kernel.Bind(typeof(IAnalyzeResults<,,>)).To<StandardResultAnalyzer>()
                .WithConstructorArgument(options.PrintInterval);
            Kernel.Bind(typeof(BaseGame<,,,>)).To<StandardGame>();
        }

        private void LoadEnvironmental(Either<string, int> cellGridConstructorArgument, CommandOptions options)
        {
            cellGridConstructorArgument.Switch(
                cellRep => Kernel.Bind<EnvironmentalCellGrid>().ToSelf().WithConstructorArgument(cellRep),
                size => Kernel.Bind<EnvironmentalCellGrid>().ToSelf().WithConstructorArgument(size));

            Kernel.Bind<SeasonalTime>().ToConstant(SeasonalTime.Spring);
            Kernel.Bind<EnvironmentalWorldData>().ToSelf()
                .WithPropertyValue(nameof(EnvironmentalWorldData.Temperature), options.Temperature);
            Kernel.Bind(typeof(ICalculateCell<,,>)).To<EnvironmentalCellCalculator>();
            Kernel.Bind(typeof(IGenerateWorld<,,,>)).To<EnvironmentalWorldGenerator>();

            switch (options.NeighbourFinder)
            {
                case NeighbourFinderType.None:
                    throw new InvalidOperationException(nameof(NeighbourFinderType));
                case NeighbourFinderType.Closed:
                    Kernel.Bind(typeof(IFindNeighbours<,>)).To<ClosedNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>();
                    break;
                case NeighbourFinderType.Open:
                    Kernel.Bind(typeof(IFindNeighbours<,>)).To<OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>();
                    break;
            }

            switch (options.Seasons)
            {
                case SeasonCalculatorType.None:
                    throw new InvalidOperationException(nameof(SeasonCalculatorType));
                case SeasonCalculatorType.Ignore:
                    Kernel.Bind<ICalculateSeason>().To<NoSeasonCalculator>();
                    break;
                case SeasonCalculatorType.Standard:
                    Kernel.Bind<ICalculateSeason>().To<StandardSeasonCalculator>();
                    break;
            }

            Kernel.Bind(typeof(BaseWorld<,,,>)).To<EnvironmentalWorld>();
            Kernel.Bind(typeof(IRenderer<,,>)).To<EnvironmentalConsoleRenderer>();
            Kernel.Bind(typeof(IAnalyzeResults<,,>)).To<EnvironmentalResultAnalyzer>()
                .WithConstructorArgument(options.PrintInterval)
                .WithConstructorArgument(options.PrintFile);
            Kernel.Bind(typeof(BaseGame<,,,>)).To<EnvironmentalGame>();
        }
    }
}