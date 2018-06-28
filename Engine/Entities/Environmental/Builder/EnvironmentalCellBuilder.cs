using System;
using System.Collections.Generic;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Environmental.Builder
{
    public class EnvironmentalCellBuilder : GenericBuilder<EnvironmentalCell>
    {
        public EnvironmentalCellBuilder() : base(new EnvironmentalCell())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>();
        }

        public EnvironmentalCellBuilder(EnvironmentalCell initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}