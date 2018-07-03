using System;
using System.Collections.Generic;

using Engine.Entities.Environmental;
using Engine.Entities.Standard;

using GameOfLife.Entities;
using GameOfLife.Game;
using GameOfLife.Helpers;
using GameOfLife.ResultAnalyzer;

using Graphics.Renderer;

using Tp.Core;

namespace GameOfLife.Mappers
{
    public class GeneralTypeMapper
    {
        public readonly IReadOnlyDictionary<GameType, Func<Either<StandardConsoleRenderer, EnvironmentalConsoleRenderer>>> ConsoleRendererMap;

        public readonly IReadOnlyDictionary<GameType, Func<
            int,
            Maybe<string>,
            Either<StandardResultAnalyzer, EnvironmentalResultAnalyzer>>> ResultAnalyzerMap;

        public readonly IReadOnlyDictionary<GameType, Func<
            Either<IRenderer<StandardCell, StandardCellGrid, StandardWorldData>, IRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>>,
            Either<IAnalyzeResults<StandardCell, StandardCellGrid, StandardWorldData>, IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>>,
            Either<StandardGame, EnvironmentalGame>>> GameMapper;

        public GeneralTypeMapper()
        {
            ConsoleRendererMap = new Dictionary<GameType, Func<Either<StandardConsoleRenderer, EnvironmentalConsoleRenderer>>>
            {
                { GameType.Standard, () => Either.CreateLeft<StandardConsoleRenderer, EnvironmentalConsoleRenderer>(new StandardConsoleRenderer()) },
                { GameType.Environmental, () => Either.CreateRight<StandardConsoleRenderer, EnvironmentalConsoleRenderer>(new EnvironmentalConsoleRenderer()) }
            };

            ResultAnalyzerMap = new Dictionary<GameType, Func<int, Maybe<string>, Either<StandardResultAnalyzer, EnvironmentalResultAnalyzer>>>
            {
                { GameType.Standard, (interval, _) => Either.CreateLeft<StandardResultAnalyzer, EnvironmentalResultAnalyzer>(new StandardResultAnalyzer(interval)) },
                { GameType.Environmental, (interval, filePath) => Either.CreateRight<StandardResultAnalyzer, EnvironmentalResultAnalyzer>(new EnvironmentalResultAnalyzer(interval, filePath.Value)) }
            };

            GameMapper = new Dictionary<GameType, Func<Either<IRenderer<StandardCell, StandardCellGrid, StandardWorldData>, IRenderer<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>>, Either<IAnalyzeResults<StandardCell, StandardCellGrid, StandardWorldData>, IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>>, Either<StandardGame, EnvironmentalGame>>>
            {
                { GameType.Standard, (renderer, analyzer) =>  Either.CreateLeft<StandardGame, EnvironmentalGame>(new StandardGame(renderer.Left(), analyzer.Left())) },
                { GameType.Environmental, (renderer, analyzer) => Either.CreateRight<StandardGame, EnvironmentalGame>(new EnvironmentalGame(renderer.Right(), analyzer.Right())) }
            };
        }
    }
}