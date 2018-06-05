using System.Diagnostics;
using GameOfLife.Core;

namespace GameOfLife.Renderer
{
    public interface IRenderer
    {
        Stopwatch GenerationWatch { get; }

        void PrintTick(IWorld tick);
        void PrintGeneration(int generation);
    }
}