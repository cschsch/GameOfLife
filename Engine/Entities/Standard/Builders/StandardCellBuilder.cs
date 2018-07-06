using System;
using System.Collections.Generic;

using Engine.Helpers;

namespace Engine.Entities.Standard.Builders
{
    public class StandardCellBuilder : GenericBuilder<StandardCell>
    {
        public StandardCellBuilder() : base(new StandardCell())
        {
            DefaultValues = new Dictionary<string, Func<dynamic>>();
        }
    }
}