using System;
using System.Diagnostics;
using System.Linq;
using Engine.Entities;
using Engine.Helpers;
using Graphics.Renderer.FastConsole;

namespace Graphics.Renderer
{
    public abstract class BaseConsoleRenderer<TCell, TCellGrid, TWorldData> : IRenderer<TCell, TCellGrid, TWorldData>
        where TCell : BaseCell
        where TCellGrid : BaseCellGrid<TCell>
        where TWorldData : BaseWorldData<TCell, TCellGrid>
    {
        public Stopwatch GenerationWatch { get; }
        private Match<TCell, char> CellRepresentation { get; }

        protected BaseConsoleRenderer(Match<TCell, char> cellRepresentation)
        {
            Console.Clear();
            GenerationWatch = new Stopwatch();
            CellRepresentation = cellRepresentation;
        }

        public void PrintUi(TWorldData data)
        {
            PrintGrid(data.Grid);
            PrintGeneration(data.Generation, data.Grid.Cells.Count);
        }

        public void PrintGrid(TCellGrid grid) => QuickWrite.Write(grid.Cells.SelectMany(row => row.Select(cell =>
        {
            var representation = CellRepresentation.MatchFirst(cell);

            return new Kernel32.CharInfo
            {
                Attributes = (short) GetColorFromLifetime(cell),
                Char = new Kernel32.CharUnion { UnicodeChar = representation }
            };
        })), (short) grid.Cells.Count);

        private ConsoleColor GetColorFromLifetime(TCell cell) =>
            new Match<TCell, ConsoleColor>(
                (c => !c.IsAlive, _ => ConsoleColor.White),
                (c => c.LifeTime == 1, _ => ConsoleColor.Red),
                (c => c.LifeTime <= 3, _ => ConsoleColor.Yellow),
                (c => c.LifeTime <= 5, _ => ConsoleColor.Green),
                (_ => true, _ => ConsoleColor.Cyan)).MatchFirst(cell);

        private void PrintGeneration(int generation, int worldSize)
        {
            Console.SetCursorPosition(0, worldSize + 1);
            Console.Write($"Generation {generation}{Environment.NewLine}Generations/sec: {generation / GenerationWatch.Elapsed.TotalSeconds}");
        }
    }
}