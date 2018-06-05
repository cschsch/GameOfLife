using System.Diagnostics;
using GameOfLife.Core;

namespace GameOfLife.Renderer
{
    public interface IRenderer
    {
        Stopwatch GenerationWatch { get; }

        void PrintTick(World tick);
        void PrintGeneration(int generation);
    }
}