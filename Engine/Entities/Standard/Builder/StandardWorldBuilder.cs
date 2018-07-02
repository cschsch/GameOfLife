using System;
using System.Collections.Generic;
using Engine.Strategies.CalculatorStrategies;
using Engine.Strategies.GeneratorStrategies;
using Engine.Strategies.NeighbourStrategies;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Standard.Builder
{
    public class StandardWorldBuilder : GenericBuilder<StandardWorld>
    {
        public StandardWorldBuilder() : base(new StandardWorld())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>
            {
                {GenericExtensions.GetPropertyName<StandardWorld, StandardWorldData>(w => w.Data), () => new StandardWorldData() },
                {GenericExtensions.GetPropertyName<StandardWorld, IFindNeighbours<StandardCell, StandardCellGrid>>(w => w.NeighbourFinder), () => new OpenNeighbourFinder<StandardCell, StandardCellGrid>() },
                {GenericExtensions.GetPropertyName<StandardWorld, ICalculateCell<StandardCell, StandardCellGrid, StandardWorldData>>(w => w.CellCalculator), () => new StandardCellCalculator() },
                {GenericExtensions.GetPropertyName<StandardWorld, IGenerateWorld<StandardCell, StandardCellGrid, StandardWorldData, StandardWorld>>(w => w.WorldGenerator), () => new StandardWorldGenerator() }
            };
        }

        public StandardWorldBuilder(StandardWorld initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}