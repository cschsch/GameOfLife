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
            var alive = new int[(int) DietaryRestriction.End];
            var aliveInTotal = 0;
            foreach (var cells in neighbours.Where(n => n.IsAlive).GroupBy(n => n.Diet))
            {
                var cellCount = cells.Count();
                alive[(int) cells.Key] = cellCount;
                aliveInTotal += cellCount;
            }
            
            var differenceOfNeighboursByDiet = alive[(int) DietaryRestriction.Carnivore] - alive[(int) DietaryRestriction.Herbivore];

            var transformsToCarnivorePropability = data.HerbivoreDensity * (data.Temperature.DivideSkipZeroDivisor(1 + data.Temperature));
            var transformsToCarnivore = RandomNumberGenerator.NextBool(transformsToCarnivorePropability);
            var diet = cell.Diet == DietaryRestriction.Herbivore && transformsToCarnivore && differenceOfNeighboursByDiet >= 2
                ? DietaryRestriction.Carnivore
                : cell.Diet;

            return Match<EnvironmentalCell, EnvironmentalCell>.MatchFirst(cell,
                    (cMatch => !cMatch.IsAlive && aliveInTotal == 3, _ => EnvironmentalFlyweightCellFactory.GetEnvironmentalCell($"Alive{diet}")),
                    (_ => aliveInTotal < 2 || aliveInTotal > 3, _ => EnvironmentalFlyweightCellFactory.GetEnvironmentalCell($"Dead{diet}")),
                    (_ => true, cMatch => new EnvironmentalCell(cMatch) {LifeTime = cMatch.LifeTime + 1, Diet = diet}));
        }
    }
}