using System;
using System.Collections.Generic;
using Engine.Strategies.CalculatorStrategies;
using Engine.Strategies.GeneratorStrategies;
using Engine.Strategies.NeighbourStrategies;
using Engine.Entities.Environmental;
using Engine.Entities.Standard;
using GameOfLife.Entities;
using GameOfLife.Helpers;
using Tp.Core;

namespace GameOfLife.Mapper
{
    public class EngineTypeMapper
    {
        public readonly IReadOnlyDictionary<GameType, Func<Either<string, int>, Either<StandardCellGrid, EnvironmentalCellGrid>>> CellGridTypeMap;
        public readonly IReadOnlyDictionary<GameType, Func<GenericEitherBuilder<StandardWorldData, EnvironmentalWorldData>>> WorldDataTypeMap;
        public readonly IReadOnlyDictionary<GameType, Func<GenericEitherBuilder<StandardWorld, EnvironmentalWorld>>> WorldTypeMap;

        public readonly IReadOnlyDictionary<GameType, Func<Either<IFindNeighbours<StandardCell, StandardCellGrid>, IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>>>> OpenNeighbourFinderMap;
        public readonly IReadOnlyDictionary<GameType, Func<Either<IFindNeighbours<StandardCell, StandardCellGrid>, IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>>>> ClosedNeighbourFinderMap;

        public readonly IReadOnlyDictionary<GameType, Func<Either<StandardCellCalculator, EnvironmentalCellCalculator>>> CellCalculatorMap;

        public readonly IReadOnlyDictionary<GameType, Func<Either<StandardWorldGenerator, EnvironmentalWorldGenerator>>> WorldGeneratorMap;

        public EngineTypeMapper()
        {
            CellGridTypeMap = new Dictionary<GameType, Func<Either<string, int>, Either<StandardCellGrid, EnvironmentalCellGrid>>>
            {
                {
                    GameType.Standard, cellRepOrSize => cellRepOrSize.Switch(
                        cellRep => Either.CreateLeft<StandardCellGrid, EnvironmentalCellGrid>(new StandardCellGrid(cellRep)),
                        size => Either.CreateLeft<StandardCellGrid, EnvironmentalCellGrid>(new StandardCellGrid(size)))
                },
                {
                    GameType.Environmental, cellRepOrSize => cellRepOrSize.Switch(
                        cellRep => Either.CreateRight<StandardCellGrid, EnvironmentalCellGrid>(new EnvironmentalCellGrid(cellRep)),
                        size => Either.CreateRight<StandardCellGrid, EnvironmentalCellGrid>(new EnvironmentalCellGrid(size)))
                }
            };

            WorldDataTypeMap = new Dictionary<GameType, Func<GenericEitherBuilder<StandardWorldData, EnvironmentalWorldData>>>
            {
                { GameType.Standard, () => new GenericEitherBuilder<StandardWorldData, EnvironmentalWorldData>(Either.CreateLeft<StandardWorldData, EnvironmentalWorldData>(new StandardWorldData())) },
                { GameType.Environmental, () => new GenericEitherBuilder<StandardWorldData, EnvironmentalWorldData>(Either.CreateRight<StandardWorldData, EnvironmentalWorldData>(new EnvironmentalWorldData())) }
            };

            WorldTypeMap = new Dictionary<GameType, Func<GenericEitherBuilder<StandardWorld, EnvironmentalWorld>>>
            {
                { GameType.Standard, () => new GenericEitherBuilder<StandardWorld, EnvironmentalWorld>( Either.CreateLeft<StandardWorld, EnvironmentalWorld>(new StandardWorld())) },
                { GameType.Environmental, () => new GenericEitherBuilder<StandardWorld, EnvironmentalWorld>( Either.CreateRight<StandardWorld, EnvironmentalWorld>(new EnvironmentalWorld())) }
            };

            OpenNeighbourFinderMap = new Dictionary<GameType, Func<Either<IFindNeighbours<StandardCell, StandardCellGrid>, IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>>>>
            {
                { GameType.Standard, () => Either.CreateLeft<OpenNeighbourFinder<StandardCell, StandardCellGrid>, OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>(new OpenNeighbourFinder<StandardCell, StandardCellGrid>()) },
                { GameType.Environmental, () => Either.CreateRight<OpenNeighbourFinder<StandardCell, StandardCellGrid>, OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>(new OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>()) }
            };

            ClosedNeighbourFinderMap = new Dictionary<GameType, Func<Either<IFindNeighbours<StandardCell, StandardCellGrid>, IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>>>>
            {
                { GameType.Standard, () => Either.CreateLeft<ClosedNeighbourFinder<StandardCell, StandardCellGrid>, ClosedNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>(new ClosedNeighbourFinder<StandardCell, StandardCellGrid>()) },
                { GameType.Environmental, () => Either.CreateRight<ClosedNeighbourFinder<StandardCell, StandardCellGrid>, ClosedNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>>(new ClosedNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>()) }
            };

            CellCalculatorMap = new Dictionary<GameType, Func<Either<StandardCellCalculator, EnvironmentalCellCalculator>>>
            {
                { GameType.Standard, () => Either.CreateLeft<StandardCellCalculator, EnvironmentalCellCalculator>(new StandardCellCalculator()) },
                { GameType.Environmental, () => Either.CreateRight<StandardCellCalculator, EnvironmentalCellCalculator>(new EnvironmentalCellCalculator(new Random())) }
            };

            WorldGeneratorMap = new Dictionary<GameType, Func<Either<StandardWorldGenerator, EnvironmentalWorldGenerator>>>
            {
                { GameType.Standard, () => Either.CreateLeft<StandardWorldGenerator, EnvironmentalWorldGenerator>(new StandardWorldGenerator()) },
                { GameType.Environmental, () => Either.CreateRight<StandardWorldGenerator, EnvironmentalWorldGenerator>(new EnvironmentalWorldGenerator()) }
            };
        }
    }
}