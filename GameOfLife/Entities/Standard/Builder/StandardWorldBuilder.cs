using System.Collections.Generic;
using GameOfLife.Core.CalculatorStrategies;
using GameOfLife.Core.GeneratorStrategies;
using GameOfLife.Core.NeighbourStrategies;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Standard.Builder
{
    public class StandardWorldBuilder : GenericBuilder<StandardWorld>
    {
        public StandardWorldBuilder() : base(new StandardWorld())
        {
            DefaultValues = new Dictionary<string, dynamic>
            {
                {GenericExtensions.GetPropertyName<StandardWorld, StandardWorldData>(w => w.Data), new StandardWorldData() },
                {GenericExtensions.GetPropertyName<StandardWorld, IFindNeighbours<StandardCell, StandardCellGrid>>(w => w.NeighbourFinder), new OpenNeighbourFinder<StandardCell, StandardCellGrid>() },
                {GenericExtensions.GetPropertyName<StandardWorld, ICalculateCell<StandardCell, StandardCellGrid, StandardWorldData>>(w => w.CellCalculator), new StandardCellCalculator() },
                {GenericExtensions.GetPropertyName<StandardWorld, IGenerateWorld<StandardCell, StandardCellGrid, StandardWorldData, StandardWorld>>(w => w.WorldGenerator), new StandardWorldGenerator() }
            };
        }

        public StandardWorldBuilder(StandardWorld initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}