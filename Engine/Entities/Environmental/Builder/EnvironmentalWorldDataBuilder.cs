using System;
using System.Collections.Generic;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Environmental.Builder
{
    public class EnvironmentalWorldDataBuilder : GenericBuilder<EnvironmentalWorldData>
    {
        public EnvironmentalWorldDataBuilder() : base(new EnvironmentalWorldData())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>
            {
                {GenericExtensions.GetPropertyName<EnvironmentalWorldData, EnvironmentalCellGrid>(wd => wd.Grid), () => new EnvironmentalCellGrid(0)}
            };
        }

        public EnvironmentalWorldDataBuilder(EnvironmentalWorldData initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}