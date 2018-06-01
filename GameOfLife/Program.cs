using System;
using System.Diagnostics;
using System.Linq;
using CommandLine;
using GameOfLife.Core;
using GameOfLife.FastConsole;
using GameOfLife.Helpers;
using Tp.Core;

namespace GameOfLife
{
    internal static class Program
    {
        private static Stopwatch GenerationWatch { get; } = new Stopwatch();

        public static void Main(string[] args)
        {
            var worldOrError = Parser.Default.ParseArguments<CommandOptions>(args).MapResult(
                opts => Either.CreateLeft<(IWorld, int), string>(MapArgs(opts)),
                err => Either.CreateRight<(IWorld, int), string>(string.Join(", ", err)));

            worldOrError.Switch(world =>
            {
                Console.Clear();
                GenerationWatch.Start();
                GameLoop(world);
            }, Console.Write);
        }

        private static (IWorld, int) MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            if (figures.Any(f => string.Equals(f.Key, opts.FigureName, StringComparison.InvariantCultureIgnoreCase)))
                return (new RoundWorld(figures[opts.FigureName.ToUpper()]), opts.ThreadSleep);

            var world = opts.Closed ? ClosedWorld.NewWorld(opts.Size) : RoundWorld.NewWorld(opts.Size);
            return (world(), opts.Size);
        }

        private static void GameLoop((IWorld, int) args)
        {
            var (world, sleep) = args;
            var generation = 1;
            while (true)
            {
                (world, generation) = PrintOneHundredGenerations(world, sleep, generation);
            }
        }

        private static (IWorld, int) PrintOneHundredGenerations(IWorld lastWorld, int sleepInMs, int generation)
        {
            var tickToReturn = lastWorld;

            foreach (var tick in lastWorld.Ticks().Take(100))
            {
                var buffer = tick.Cells.SelectMany(row => row.Select(cell => new Kernel32.CharInfo
                {
                    Attributes = (short)GetColorFromLifetime(cell),
                    Char = new Kernel32.CharUnion { UnicodeChar = cell.ToString().Single() }
                }));
                QuickWrite.Write(buffer, (short) tick.Cells.Length);
                //QuickWrite.Write(tick.ToString(), (short) tick.Cells.Length);
                tickToReturn = tick;

                generation++;
                Console.SetCursorPosition(0, tickToReturn.Cells.Length + 1);
                Console.Write($"Generation {generation}{Environment.NewLine}Generations/sec: {generation / GenerationWatch.Elapsed.TotalSeconds}");

                System.Threading.Thread.Sleep(sleepInMs);
            }

            return (tickToReturn, generation);
        }

        private static ConsoleColor GetColorFromLifetime(Cell cell) =>
            new Match<Cell, ConsoleColor>(
                (c => !c.IsAlive, _ => ConsoleColor.White),
                (c => c.LifeTime == 1, _ => ConsoleColor.Red),
                (c => c.LifeTime == 2, _ => ConsoleColor.Yellow),
                (c => c.LifeTime == 3, _ => ConsoleColor.Green),
                (_ => true, _ => ConsoleColor.Cyan)).MatchFirst(cell);
    }

    internal class CommandOptions
    {
        [Option('s', "size", Default = 70)]
        public int Size { get; set; }

        [Option('f', "figure")]
        public string FigureName { get; set; }

        [Option('t', "thread_sleep")]
        public int ThreadSleep { get; set; }

        [Option('c', "closed")]
        public bool Closed { get; set; }
    }
}
