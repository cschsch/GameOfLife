using System.Collections.Generic;

namespace Engine.Entities
{
    public abstract class BaseCellGrid<TCell> where TCell : BaseCell
    {
        public IReadOnlyList<IReadOnlyList<TCell>> Cells { get; protected set; }
    }
}