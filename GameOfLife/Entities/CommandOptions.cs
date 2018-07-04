using CommandLine;

using GameOfLife.Entities.Enums;

namespace GameOfLife.Entities
{
    internal class CommandOptions
    {
        [Option("game_type", Default = GameType.Environmental)]
        public GameType GameType { get; set; }

        [Option('s', "size", Default = 69)]
        public int Size { get; set; }

        [Option('f', "figure", Default = "")]
        public string FigureName { get; set; }

        [Option("file", Default = "")]
        public string FilePath { get; set; }

        [Option('t', "thread_sleep")]
        public int ThreadSleep { get; set; }

        [Option('n', "neighbour_finder", Default = NeighbourFinderType.Open)]
        public NeighbourFinderType NeighbourFinder { get; set; }

        [Option("temperature")]
        public double Temperature { get; set; }

        [Option('i', "print_interval", Default = 100)]
        public int PrintInterval { get; set; }

        [Option('p', "print_file", Default = @"analyzation\01.txt")]
        public string PrintFile { get; set; }

        [Option("season_calculator", Default = SeasonCalculatorType.Ignore)]
        public SeasonCalculatorType Seasons { get; set; }
    }
}