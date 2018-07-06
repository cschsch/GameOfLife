using System;
using System.Collections.Generic;

using Engine.Helpers;

namespace Engine.Entities.Environmental.Builders
{
    public class EnvironmentalCellBuilder : GenericBuilder<EnvironmentalCell>
    {
        public EnvironmentalCellBuilder() : base(new EnvironmentalCell())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>();
        }
    }
}