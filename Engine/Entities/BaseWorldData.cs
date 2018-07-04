using Engine.Helpers;

namespace Engine.Entities
{
    public abstract class BaseWorldData<TCell, TCellGrid>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
    {
        public int Generation { get; set; }

        [Dependency]
        public TCellGrid Grid { get; set; }
    }
}