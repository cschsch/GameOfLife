using System;
using System.Collections.Generic;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Entities.Standard.Builder
{
    public class StandardCellBuilder : GenericBuilder<StandardCell>
    {
        public StandardCellBuilder() : base(new StandardCell())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>();
        }

        public StandardCellBuilder(StandardCell initial) : this() => ObjectToBuild.SetProperties(initial);
    }
}