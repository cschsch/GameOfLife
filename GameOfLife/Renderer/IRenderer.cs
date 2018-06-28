using System.Diagnostics;
using GameOfLife.Entities;

namespace GameOfLife.Renderer
{
    public interface IRenderer<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        Stopwatch GenerationWatch { get; }

        void PrintUi(TWorldData data);
    }
}