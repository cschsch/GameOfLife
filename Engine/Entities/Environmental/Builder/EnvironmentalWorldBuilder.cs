using System;
using System.Collections.Generic;
using Engine.Core.CalculatorStrategies;
using Engine.Core.GeneratorStrategies;
using Engine.Core.NeighbourStrategies;
using Engine.Entities.Standard;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Environmental.Builder
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
                {GenericExtensions.GetPropertyName<EnvironmentalWorld, IGenerateWorld<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData, EnvironmentalWorld>>(w => w.WorldGenerator), () => new EnvironmentalWorldGenerator() }
            };
        }

        public EnvironmentalWorldBuilder(EnvironmentalWorld initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}