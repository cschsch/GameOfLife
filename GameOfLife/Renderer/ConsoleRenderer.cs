using System;
using System.Diagnostics;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Renderer.FastConsole;
using GameOfLife.Helpers;

namespace GameOfLife.Renderer
{
    public class ConsoleRenderer : IRenderer
    {
        public Stopwatch GenerationWatch { get; }
        private int WorldSize { get; }
        private (char carnivore, char herbivore) CellRep { get; }

        public ConsoleRenderer(int worldSize, (char carnivore, char herbivore) cellRep)
        {
            Console.Clear();
            WorldSize = worldSize;
            GenerationWatch = new Stopwatch();
            CellRep = cellRep;
        }

        public void PrintUi(WorldData data)
        {
            PrintGrid(data.Grid);
            PrintGeneration(data.Generation);
        }

        public void PrintGrid(CellGrid grid) => QuickWrite.Write(grid.Cells.SelectMany(row => row.Select(cell =>
        {
            var representation = cell.Diet == DietaryRestrictions.Carnivore ? CellRep.carnivore : CellRep.herbivore;
            representation = cell.IsAlive ? representation : ' ';

            return new Kernel32.CharInfo
            {
                Attributes = (short) GetColorFromLifetime(cell),
                Char = new Kernel32.CharUnion {UnicodeChar = representation}
            };
        })), (short) grid.Cells.Count);

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