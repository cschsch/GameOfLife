using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities.Environmental;
using GameOfLife.Entities.Environmental.Builder;
using GameOfLife.Helpers;
using GameOfLife.Helpers.Functions;

namespace GameOfLife.Core.CalculatorStrategies
{
    public class EnvironmentalCellCalculator : ICalculateCell<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>
    {
        private Random RandomNumberGenerator { get; }

        public EnvironmentalCellCalculator(Random randomNumberGenerator) =>
            RandomNumberGenerator = randomNumberGenerator;

        public EnvironmentalCell CalculateCell(EnvironmentalCell cell, IEnumerable<EnvironmentalCell> neighbours, EnvironmentalWorldData data)
        {
            var aliveByDiet = neighbours
                .Where(n => n.IsAlive)
                .GroupBy(n => n.Diet)
                .ToDictionary(g => g.Key, g => g.Count());
            var aliveInTotal = aliveByDiet.Sum(kv => kv.Value);

            var differenceOfNeighboursByDiet = aliveByDiet.GetValueOrDefault(DietaryRestrictions.Carnivore) - aliveByDiet.GetValueOrDefault(DietaryRestrictions.Herbivore);

            var transformsToCarnivorePropability = data.HerbivoreDensity * (data.Temperature / (1 + data.Temperature));
            var transformsToCarnivore = RandomNumberGenerator.NextBool(transformsToCarnivorePropability);

            if (cell.Diet == DietaryRestrictions.Herbivore && transformsToCarnivore && differenceOfNeighboursByDiet >= 2)
                cell.Diet = DietaryRestrictions.Carnivore;

            return new Match<EnvironmentalCell, EnvironmentalCell>(
                    (cMatch => !cMatch.IsAlive && aliveInTotal == 3, cMatch => new EnvironmentalCellBuilder(cMatch).With(c => c.IsAlive, true).With(c => c.LifeTime, 1).Create()),
                    (_ => aliveInTotal < 2 || aliveInTotal > 3, cMatch => new EnvironmentalCellBuilder(cMatch).With(c => c.IsAlive, false).With(c => c.LifeTime, 0).Create()),
                    (_ => true, cMatch => new EnvironmentalCellBuilder(cMatch).With(c => c.LifeTime, cMatch.LifeTime + 1).Create()))
                .MatchFirst(cell);
        }
    }
}