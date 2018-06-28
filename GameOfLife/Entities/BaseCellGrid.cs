using System.Collections.Generic;

namespace GameOfLife.Entities
{
    public abstract class BaseCellGrid<TCell> where TCell : BaseCell
    {
        public IReadOnlyList<IReadOnlyList<TCell>> Cells { get; protected set; }
    }
}