using System;
using System.Collections.Generic;

using Engine.Entities.Standard;
using Engine.Helpers;
using Engine.Helpers.Functions;
using Engine.Strategies.CalculatorStrategies;
using Engine.Strategies.GeneratorStrategies;
using Engine.Strategies.NeighbourStrategies;
using Engine.Strategies.SeasonStrategies;

namespace Engine.Entities.Environmental.Builders
{
    public class EnvironmentalWorldBuilder : GenericBuilder<EnvironmentalWorld>
    {
        public EnvironmentalWorldBuilder() : base(new EnvironmentalWorld())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>
            {
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, EnvironmentalWorldData>(w => w.Data), () => new StandardWorldData() },
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, IFindNeighbours<EnvironmentalCell, EnvironmentalCellGrid>>(w => w.NeighbourFinder), () => new OpenNeighbourFinder<EnvironmentalCell, EnvironmentalCellGrid>() },
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, ICalculateCell<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>>(w => w.CellCalculator), () => new EnvironmentalCellCalculator(new Random()) },
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, IGenerateWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>>(w => w.WorldGenerator), () => new EnvironmentalWorldGenerator() },
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, ICalculateSeason>(w => w.SeasonCalculator), () => new NoSeasonCalculator() }
            };
        }

        public EnvironmentalWorldBuilder(EnvironmentalWorld initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}