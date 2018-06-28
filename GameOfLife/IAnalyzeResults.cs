using System.Threading.Tasks;
using Engine.Entities;

namespace GameOfLife
{
    /// <summary>
    /// Analyzes data of each given world and prints the results
    /// </summary>
    /// <typeparam name="TCell">Type of cell</typeparam>
    /// <typeparam name="TCellGrid">Type of cell grid</typeparam>
    /// <typeparam name="TWorldData">Type of world data</typeparam>
    public interface IAnalyzeResults<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        /// <summary>
        /// Interval in ticks in which data gets printed
        /// </summary>
        int PrintInterval { get; }

        /// <summary>
        /// Collects <paramref name="data"/> to include in printed summary
        /// </summary>
        /// <param name="data">World data to collect</param>
        void CollectData(TWorldData data);

        /// <summary>
        /// Prints collected data
        /// </summary>
        void PrintResults();

        /// <summary>
        /// Asynchronously prints collected data
        /// </summary>
        /// <returns></returns>
        Task PrintResultsAsync();
    }
}