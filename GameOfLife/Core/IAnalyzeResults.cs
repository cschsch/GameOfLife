using System.Threading.Tasks;
using GameOfLife.Entities;

namespace GameOfLife.Core
{
    public interface IAnalyzeResults<TCell, TCellGrid, in TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        int PrintInterval { get; }

        void CollectData(TWorldData data);
        void PrintResults();
        Task PrintResultsAsync();
    }
}