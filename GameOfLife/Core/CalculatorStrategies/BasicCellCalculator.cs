using System.Collections.Generic;
using System.Linq;
using GameOfLife.Entities;
using GameOfLife.Entities.Builder;
using GameOfLife.Helpers;

namespace GameOfLife.Core.CalculatorStrategies
{
    public class BasicCellCalculator : ICalculateCell
    {
        public Cell CalculateCell(Cell cell, IEnumerable<Cell> neighbours, WorldData data)
        {
            var alive = neighbours.Count(n => n.IsAlive);

            return new Match<Cell, Cell>(
                    (c => !c.IsAlive && alive == 3, c => c.ToAlive()),
                    (_ => alive < 2 || alive > 3, c => c.Kill()),
                    (_ => true, c => c.IncrementLifetime()))
                .MatchFirst(cell);
        }
    }
}