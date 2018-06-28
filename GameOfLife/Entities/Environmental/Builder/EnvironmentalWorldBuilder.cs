using System;
using System.Collections.Generic;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Entities.Standard;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Environmental.Builder
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