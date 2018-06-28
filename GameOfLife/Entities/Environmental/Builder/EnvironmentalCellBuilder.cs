using System.Collections.Generic;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Environmental.Builder
{
    public class EnvironmentalCellBuilder : GenericBuilder<EnvironmentalCell>
    {
        public EnvironmentalCellBuilder() : base(new EnvironmentalCell())
        {
            DefaultValues = new Dictionary<string, dynamic>();
        }

        public EnvironmentalCellBuilder(EnvironmentalCell initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}