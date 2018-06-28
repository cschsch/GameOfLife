using System.Collections.Generic;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Environmental.Builder
{
    public class EnvironmentalWorldDataBuilder : GenericBuilder<EnvironmentalWorldData>
    {
        public EnvironmentalWorldDataBuilder() : base(new EnvironmentalWorldData())
        {
            DefaultValues = new Dictionary<string, dynamic>
            {
                {GenericExtensions.GetPropertyName<EnvironmentalWorldData, EnvironmentalCellGrid>(wd => wd.Grid), new EnvironmentalCellGrid(0)}
            };
        }

        public EnvironmentalWorldDataBuilder(EnvironmentalWorldData initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}