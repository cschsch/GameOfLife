﻿using System;
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
                opts => Either.CreateLeft<GameVariables, string>(MapArgs(opts)),
                err => Either.CreateRight<GameVariables, string>(string.Join(", ", err)));

            worldOrError.Switch(StartGame, Console.Write);
        }

        private static GameVariables MapArgs(CommandOptions opts)
        {
            var figures = typeof(Figures).GetProperties().ToDictionary(p => p.Name.ToUpper(), p => (string) p.GetValue(p));
            var cellRepresentation = (opts.Alive, opts.Dead);
            var world = figures.Any(f => string.Equals(f.Key, opts.FigureName, StringComparison.InvariantCultureIgnoreCase))
                ? new RoundWorld(figures[opts.FigureName.ToUpper()])
                : opts.Closed
                    ? (IWorld) new ClosedWorld(opts.Size)
                    : new RoundWorld(opts.Size);

            return new GameVariables{World = world, ThreadSleep = opts.ThreadSleep, CellRepresentation = cellRepresentation};
        }

        private static void StartGame(GameVariables variables)
        {
            var renderer = new ConsoleRenderer(variables.World.Cells.Length, variables.CellRepresentation);
            var game = new Game(renderer);
            game.Init();
            game.GameLoop((variables.World, variables.ThreadSleep));
        }
    }

    internal struct GameVariables
    {
        public IWorld World { get; set; }
        public int ThreadSleep { get; set; }
        public (char, char) CellRepresentation { get; set; }
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
