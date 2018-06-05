using System;
using System.Linq;
using CommandLine;
using GameOfLife.Core;
using Tp.Core;

namespace GameOfLife
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var worldOrError = Parser.Default.ParseArguments<CommandOptions>(args).MapResult(
                opts => Either.CreateLeft<(IWorld, int), string>(MapArgs(opts)),
                err => Either.CreateRight<(IWorld, int), string>(string.Join(", ", err)));

            worldOrError.Switch(world =>
            {
                Console.Clear();
                var game = new Game(world.Item1.Cells.Length);
                game.Init();
                game.GameLoop(world);
            }, Console.Write);
        }

        private static (IWorld, int) MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            if (figures.Any(f => string.Equals(f.Key, opts.FigureName, StringComparison.InvariantCultureIgnoreCase)))
                return (new RoundWorld(figures[opts.FigureName.ToUpper()]), opts.ThreadSleep);

            var world = opts.Closed ? ClosedWorld.NewWorld(opts.Size) : RoundWorld.NewWorld(opts.Size);
            return (world(), opts.ThreadSleep);
        }
    }

    internal class CommandOptions
    {
        [Option('s', "size", Default = 69)]
        public int Size { get; set; }

        [Option('f', "figure")]
        public string FigureName { get; set; }

        [Option('t', "thread_sleep")]
        public int ThreadSleep { get; set; }

        [Option('c', "closed")]
        public bool Closed { get; set; }
    }
}
