using System.Diagnostics;
using GameOfLife.Core;
using GameOfLife.Core.Worlds;

namespace GameOfLife.Renderer
{
    public interface IRenderer
    {
        Stopwatch GenerationWatch { get; }

        void PrintGrid(CellGrid grid);
        void PrintGeneration(int generation);
    }
}