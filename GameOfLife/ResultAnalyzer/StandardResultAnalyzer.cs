using System.Threading.Tasks;
using Engine.Entities.Standard;

namespace GameOfLife.ResultAnalyzer
{
    public class StandardResultAnalyzer : IAnalyzeResults<StandardCell, StandardCellGrid, StandardWorldData>
    {
        public int PrintInterval { get; }

        public StandardResultAnalyzer(int printInterval)
        {
            PrintInterval = printInterval;
        }

        public void CollectData(StandardWorldData data)
        {
        }

        public void PrintResults()
        {
        }

        public Task PrintResultsAsync() => Task.CompletedTask;
    }
}