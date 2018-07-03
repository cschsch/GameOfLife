using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Engine.Entities.Environmental;
using Engine.Helpers.Functions;

namespace GameOfLife.ResultAnalyzer
{
    public class EnvironmentalResultAnalyzer : IAnalyzeResults<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>
    {
        public int PrintInterval { get; }
        private string FilePath { get; }
        private List<EnvironmentalWorldData> Data { get; }

        public EnvironmentalResultAnalyzer(int printInterval, string filePath)
        {
            PrintInterval = printInterval;
            FilePath = filePath;
            Data = new List<EnvironmentalWorldData>();
        }

        public void CollectData(EnvironmentalWorldData data)
        {
            Data.Add(data);
        }

        private string GetAnswer()
        {
            var entriesToUse = Data.GetValues(Enumerable.Range(0, PrintInterval));

            var aliveByDiet = entriesToUse
                .Select(data => data.Grid.Cells
                    .SelectMany(row => row)
                    .Where(cell => cell.IsAlive)
                    .GroupBy(cell => cell.Diet)
                    .ToDictionary(g => g.Key, g => g.Count()));

            var generation = $"Generation: {entriesToUse.First().Generation} - {entriesToUse.Last().Generation}";
            var temperature = $"Temperature: {entriesToUse.Average(data => data.Temperature)}";
            var amountOfCarnivores = $"Amount of alive carnivores: {aliveByDiet.Average(dict => dict.GetValueOrDefault(DietaryRestriction.Carnivore))}";
            var amountOfHerbivores = $"Amount of alive herbivores: {aliveByDiet.Average(dict => dict.GetValueOrDefault(DietaryRestriction.Herbivore))}";
            var herbivoreDensity = $"Herbivore density: {entriesToUse.Average(data => data.HerbivoreDensity)}";
            var joined = string.Join(Environment.NewLine, generation, temperature, amountOfCarnivores, amountOfHerbivores, herbivoreDensity);
            const string entryDivider = "----------------------------------------------------";

            return string.Concat(joined, Environment.NewLine, entryDivider, Environment.NewLine);
        }

        public void PrintResults()
        {
            var answer = GetAnswer();
            Data.RemoveRange(0, PrintInterval);
            File.AppendAllText(FilePath, answer);
        }

        public async Task PrintResultsAsync()
        {
            var answer = Task.Run(() => GetAnswer());
            await File.AppendAllTextAsync(FilePath, await answer).ConfigureAwait(false);
            Data.RemoveRange(0, PrintInterval);
        }
    }
}