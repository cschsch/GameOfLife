using System;
using System.Diagnostics;
using System.Linq;
using GameOfLife.Core;
using GameOfLife.Renderer.FastConsole;
using GameOfLife.Helpers;

namespace GameOfLife.Renderer
{
    public class ConsoleRenderer : IRenderer
    {
        public Stopwatch GenerationWatch { get; }
        private int WorldSize { get; }
        private (char alive, char dead) CellRep { get; }

        public ConsoleRenderer(int worldSize, (char alive, char dead) cellRep)
        {
            Console.Clear();
            WorldSize = worldSize;
            GenerationWatch = new Stopwatch();
            CellRep = cellRep;
        }

        public void PrintTick(World tick) => QuickWrite.Write(tick.Cells.SelectMany(row => row.Select(cell =>
            new Kernel32.CharInfo
            {
                Attributes = (short) GetColorFromLifetime(cell),
                Char = new Kernel32.CharUnion {UnicodeChar = cell.IsAlive ? CellRep.alive : CellRep.dead}
            })), (short) tick.Cells.Length);

        private ConsoleColor GetColorFromLifetime(Cell cell) =>
            new Match<Cell, ConsoleColor>(
                (c => !c.IsAlive, _ => ConsoleColor.White),
                (c => c.LifeTime == 1, _ => ConsoleColor.Red),
                (c => c.LifeTime <= 3, _ => ConsoleColor.Yellow),
                (c => c.LifeTime <= 5, _ => ConsoleColor.Green),
                (_ => true, _ => ConsoleColor.Cyan)).MatchFirst(cell);

        public void PrintGeneration(int generation)
        {
            Console.SetCursorPosition(0, WorldSize + 1);
            Console.Write($"Generation {generation}{Environment.NewLine}Generations/sec: {generation / GenerationWatch.Elapsed.TotalSeconds}");
        }
    }
}