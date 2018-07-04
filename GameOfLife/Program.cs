using System;

using CommandLine;

using Engine.Entities;
using Engine.Helpers;

using GameOfLife.Entities;
using GameOfLife.Game;

using Ninject;

using Tp.Core;

namespace GameOfLife
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var worldOrError = Parser.Default.ParseArguments<CommandOptions>(args).MapResult(
                opts => Either.CreateLeft<Maybe<GameVariables>, string>(MapArgs(opts)),
                err => Either.CreateRight<Maybe<GameVariables>, string>(string.Join(", ", err)));

            worldOrError.Switch(StartGame, Console.Write);
        }

        private static Maybe<GameVariables> MapArgs(CommandOptions options)
        {
            try
            {
                using (var kernel = new StandardKernel(new NinjectSettings {InjectAttribute = typeof(Dependency)},
                    new EngineModule(options)))
                {
                    var game = (IGame) kernel.Get(typeof(BaseGame<,,,>));
                    var world = kernel.Get(typeof(BaseWorld<,,,>));

                    return new GameVariables
                    {
                        Game = game,
                        World = world,
                        ThreadSleep = options.ThreadSleep
                    };
                }
            }
            catch (InvalidOperationException ioExc)
            {
                Console.WriteLine($"Failed to resolve {ioExc.Message}");
                return Maybe<GameVariables>.Nothing;
            }
        }

        private static void StartGame(Maybe<GameVariables> variables)
        {
            if (!variables.HasValue) return;

            var value = variables.Value;
            value.Game.Init();
            value.Game.GameLoop(value.World, value.ThreadSleep);
        }
    }
}

