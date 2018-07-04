using System.Collections.Generic;

using Engine.Helpers;

namespace Engine.Entities
{
    public abstract class BaseCellGrid<TCell> where TCell : BaseCell
    {
        [Dependency]
        public IReadOnlyList<IReadOnlyList<TCell>> Cells { get; protected set; }
    }
}