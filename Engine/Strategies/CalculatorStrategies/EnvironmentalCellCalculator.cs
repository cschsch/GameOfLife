using System;
using System.Collections.Generic;
using System.Linq;

using Engine.Entities.Environmental;
using Engine.Entities.Environmental.Builders;
using Engine.Helpers;
using Engine.Helpers.Functions;

namespace Engine.Strategies.CalculatorStrategies
{
    public class EnvironmentalCellCalculator : ICalculateCell<EnvironmentalCell, EnvironmentalCellGrid, EnvironmentalWorldData>
    {
        private Random RandomNumberGenerator { get; }

        public EnvironmentalCellCalculator(Random randomNumberGenerator) =>
            RandomNumberGenerator = randomNumberGenerator;

        public EnvironmentalCellCalculator() : this(new Random()) { }

        public EnvironmentalCell CalculateCell(EnvironmentalCell cell, IEnumerable<EnvironmentalCell> neighbours, EnvironmentalWorldData data)
        {
            var aliveByDiet = neighbours
                .Where(n => n.IsAlive)
                .GroupBy(n => n.Diet)
                .ToDictionary(g => g.Key, g => g.Count());
            var aliveInTotal = aliveByDiet.Sum(kv => kv.Value);

            var differenceOfNeighboursByDiet = aliveByDiet.GetValueOrDefault(DietaryRestriction.Carnivore) - aliveByDiet.GetValueOrDefault(DietaryRestriction.Herbivore);

            var transformsToCarnivorePropability = data.HerbivoreDensity * (data.Temperature.DivideSkipZeroDivisor(1 + data.Temperature));
            var transformsToCarnivore = RandomNumberGenerator.NextBool(transformsToCarnivorePropability);
            var diet = cell.Diet == DietaryRestriction.Herbivore && transformsToCarnivore && differenceOfNeighboursByDiet >= 2
                ? DietaryRestriction.Carnivore
                : cell.Diet;

            return new Match<EnvironmentalCell, EnvironmentalCell>(
                    (cMatch => !cMatch.IsAlive && aliveInTotal == 3, _ => EnvironmentalFlyweightCellFactory.GetEnvironmentalCell($"Alive{diet}")),
                    (_ => aliveInTotal < 2 || aliveInTotal > 3, _ => EnvironmentalFlyweightCellFactory.GetEnvironmentalCell($"Dead{diet}")),
                    (_ => true, cMatch => new EnvironmentalCell(cMatch) {LifeTime = cMatch.LifeTime + 1, Diet = diet}))
                .MatchFirst(cell);
        }
    }
}