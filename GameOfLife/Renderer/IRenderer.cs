using System.Diagnostics;
using GameOfLife.Entities;

namespace GameOfLife.Renderer
{
    public interface IRenderer
    {
        Stopwatch GenerationWatch { get; }

        void PrintGrid(CellGrid grid);
        void PrintGeneration(int generation);
    }
}