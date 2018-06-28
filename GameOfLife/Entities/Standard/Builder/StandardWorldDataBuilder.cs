using System.Collections.Generic;
using System;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Standard.Builder
{
    public class StandardWorldDataBuilder : GenericBuilder<StandardWorldData>
    {
        public StandardWorldDataBuilder() : base(new StandardWorldData())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>
            {
                {GenericExtensions.GetPropertyName<StandardWorldData, StandardCellGrid>(wd => wd.Grid), () =>  new StandardCellGrid(0)}
            };
        }

        public StandardWorldDataBuilder(StandardWorldData initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}