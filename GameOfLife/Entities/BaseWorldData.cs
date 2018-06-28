namespace GameOfLife.Entities
{
    public abstract class BaseWorldData<TCell, TCellGrid>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
    {
        public int Generation { get; set; }
        public TCellGrid Grid { get; set; }
    }
}