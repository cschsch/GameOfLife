using System.Diagnostics;
using Engine.Entities;

namespace Graphics.Renderer
{
    /// <summary>
    /// Needed to visualize a world
    /// </summary>
    /// <typeparam name="TCell">Type of cell</typeparam>
    /// <typeparam name="TCellGrid">Type of cell grid</typeparam>
    /// <typeparam name="TWorldData">Type of world data</typeparam>
    public interface IRenderer<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        /// <summary>
        /// Stopwatch that starts with the game to calculate generations per second
        /// </summary>
        Stopwatch GenerationWatch { get; }

        /// <summary>
        /// Prints UI with the given <paramref name="data"/>
        /// </summary>
        /// <param name="data">Data to visualize</param>
        void PrintUi(TWorldData data);
    }
}