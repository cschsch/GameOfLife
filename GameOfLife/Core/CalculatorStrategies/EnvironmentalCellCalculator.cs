using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Helpers;

namespace GameOfLife.Core.CalculatorStrategies
{
    public class EnvironmentalCellCalculator : ICalculateCell
    {
        private Random RandomNumberGenerator { get; }

        public EnvironmentalCellCalculator(Random randomNumberGenerator) =>
            RandomNumberGenerator = randomNumberGenerator;

        public Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data)
        {
            var aliveByDiet = neighbours
                .Where(n => n.IsAlive)
                .GroupBy(n => n.Diet)
                .ToDictionary(g => g.Key, g => g.Count());
            var aliveInTotal = aliveByDiet.Sum(kv => kv.Value);

            var differenceOfNeighboursByDiet = aliveByDiet.GetValueOrDefault(DietaryRestrictions.Carnivore, 0) - aliveByDiet.GetValueOrDefault(DietaryRestrictions.Herbivore, 0);

            var transformsToCarnivorePropability = data.HerbivoreDensity * ((double) data.Temperature / (1 + data.Temperature));
            var transformsToCarnivore = RandomNumberGenerator.NextBool(transformsToCarnivorePropability);

            if (cell.Diet == DietaryRestrictions.Herbivore && transformsToCarnivore && differenceOfNeighboursByDiet >= 2)
                cell.Diet = DietaryRestrictions.Carnivore;

            return new Match<Cell, Cell>(
                    (c => !c.IsAlive && aliveInTotal == 3, c => c.ToAlive()),
                    (_ => aliveInTotal < 2 || aliveInTotal > 3, c => c.Kill()),
                    (_ => true, c => c.IncrementLifetime()))
                .MatchFirst(cell);
        }
    }
}