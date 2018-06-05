using System;
using System.Linq;
using CommandLine;
using GameOfLife.Core;
using GameOfLife.Renderer;
using Tp.Core;

namespace GameOfLife
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var worldOrError = Parser.Default.ParseArguments<CommandOptions>(args).MapResult(
                opts => Either.CreateLeft<(IWorld world, int sleep, (char, char) cellRep), string>(MapArgs(opts)),
                err => Either.CreateRight<(IWorld world, int sleep, (char, char) cellRep), string>(string.Join(", ", err)));

            worldOrError.Switch(left =>
            {
                Console.Clear();
                var game = new Game(new ConsoleRenderer(left.world.Cells.Length, left.cellRep));
                game.Init();
                game.GameLoop((left.world, left.sleep));
            }, Console.Write);
        }

        private static (IWorld world, int, (char, char)) MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            if (figures.Any(f => string.Equals(f.Key, opts.FigureName, StringComparison.InvariantCultureIgnoreCase)))
                return (new RoundWorld(figures[opts.FigureName.ToUpper()]), opts.ThreadSleep, (opts.Alive, opts.Dead));

            var world = opts.Closed ? ClosedWorld.NewWorld(opts.Size) : RoundWorld.NewWorld(opts.Size);
            return (world(), opts.ThreadSleep, (opts.Alive, opts.Dead));
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

        [Option('a', "alive", Default = 'X')]
        public char Alive { get; set; }

        [Option('d', "dead", Default = ' ')]
        public char Dead { get; set; }
    }
}
