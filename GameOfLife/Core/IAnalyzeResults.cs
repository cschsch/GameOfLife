using System.Threading.Tasks;
using GameOfLife.Entities;

namespace GameOfLife.Core
{
    public interface IAnalyzeResults
    {
        int PrintInterval { get; }

        void CollectData(WorldData data);
        void PrintResults();
        Task PrintResultsAsync();
    }
}