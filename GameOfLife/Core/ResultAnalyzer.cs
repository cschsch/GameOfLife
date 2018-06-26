using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GameOfLife.Entities;

namespace GameOfLife.Core
{
    public class ResultAnalyzer : IAnalyzeResults
    {
        public int PrintInterval { get; }
        private string FilePath { get; }
        private List<WorldData> Data { get; }

        public ResultAnalyzer(int printInterval, string filePath)
        {
            PrintInterval = printInterval;
            FilePath = filePath;
            Data = new List<WorldData>();
        }

        public void CollectData(WorldData data)
        {
            Data.Add(data);
        }

        public void PrintResults()
        {
            var generation = $"Generation: {Data[0].Generation} - {Data.Last().Generation}";
            var temperature = $"Temperature: {Data.Average(data => data.Temperature)}";
            var herbivoreDensity = $"Herbivore density: {Data.Average(data => data.HerbivoreDensity)}";
            var joined = string.Join(Environment.NewLine, generation, temperature, herbivoreDensity);

            Data.Clear();

            File.AppendAllText(FilePath, string.Concat(joined, Environment.NewLine, Environment.NewLine));
        }
    }
}