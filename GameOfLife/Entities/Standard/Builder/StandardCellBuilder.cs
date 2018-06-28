using System.Collections.Generic;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Entities.Standard.Builder
{
    public class StandardCellBuilder : GenericBuilder<StandardCell>
    {
        public StandardCellBuilder() : base(new StandardCell())
        {
            DefaultValues = new Dictionary<string, dynamic>();
        }

        public StandardCellBuilder(StandardCell initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}