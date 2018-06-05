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

            worldOrError.Switch(StartGame, Console.Write);
        }

        private static (IWorld world, int, (char, char)) MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            if (figures.Any(f => string.Equals(f.Key, opts.FigureName, StringComparison.InvariantCultureIgnoreCase)))
                return (new RoundWorld(figures[opts.FigureName.ToUpper()]), opts.ThreadSleep, (opts.Alive, opts.Dead));

            var world = opts.Closed ? (IWorld) new ClosedWorld(opts.Size) : new RoundWorld(opts.Size);
            return (world, opts.ThreadSleep, (opts.Alive, opts.Dead));
        }

        private static void StartGame((IWorld world, int sleep, (char, char) cellRep) gameVars)
        {
            var renderer = new ConsoleRenderer(gameVars.world.Cells.Length, gameVars.cellRep);
            var game = new Game(renderer);
            game.Init();
            game.GameLoop((gameVars.world, gameVars.sleep));
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
